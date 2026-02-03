using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebApi.Domain.Entities.Authentication;
using WebApi.Infrastructure.Contexts;

namespace WebApi.Infrastructure.Dependencies;

public static class AuthenticationInjection
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<WriteDbContext>();
        var projectId = configuration.GetValue<string>("Auth:ProjectId") ?? "N/A";

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.IncludeErrorDetails = configuration.GetValue<bool>("Auth:IncludeErrorDetails");
                options.Authority = configuration.GetValue<string>("Auth:Authority");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = configuration.GetValue<bool>("Auth:ValidateIssuer"),
                    ValidIssuer = configuration.GetValue<string>("Auth:ValidIssuer"),
                    ValidateAudience = configuration.GetValue<bool>("Auth:ValidateAudience"),
                    ValidAudience = projectId,
                    ValidateLifetime = configuration.GetValue<bool>("Auth:ValidateLifetime"),
                    ValidateIssuerSigningKey = configuration.GetValue<bool>("Auth:ValidateIssuerSigningKey"),
                    IssuerSigningKeys = GetTotalkeys(
                        configuration.GetValue<string>("Auth:KeysUrl") ?? string.Empty,
                        configuration.GetValue<string>("Auth:AdditionalKeysUrl") ?? string.Empty)
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        // Receive the JWT token that firebase has provided
                        var firebaseToken = context.SecurityToken as Microsoft.IdentityModel.JsonWebTokens.JsonWebToken;
                        // Get the Firebase UID of this user
                        var firebaseUid = firebaseToken?.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
                        if (!string.IsNullOrEmpty(firebaseUid))
                        {
                            // Use the Firebase UID to find or create the user in your Identity system
                            var userManager = context.HttpContext.RequestServices
                                .GetRequiredService<UserManager<ApplicationUser>>();

                            var user = await userManager.FindByNameAsync(firebaseUid);

                            if (user == null)
                            {
                                // Create a new ApplicationUser in your database if the user doesn't exist
                                user = new ApplicationUser
                                {
                                    UserName = firebaseUid,
                                    Email = firebaseToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                                    FirebaseUserId = firebaseUid,
                                    Name = firebaseToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value ??
                                           $"Planner {firebaseUid}",
                                };
                                await userManager.CreateAsync(user);
                            }
                        }
                    }
                };
            }
        );
        return services;
    }

    private static IEnumerable<SecurityKey> GetTotalkeys(string keysUrl, string additionalKeysUrl)
    {
        var client = new HttpClient();
        var keys = client
            .GetStringAsync(keysUrl)
            .Result;
        var originalKeys = new JsonWebKeySet(keys).GetSigningKeys();
        var additionalkeys = client
            .GetStringAsync(additionalKeysUrl)
            .Result;
        var morekeys = new JsonWebKeySet(additionalkeys).GetSigningKeys();
        return originalKeys.Concat(morekeys);
    }
}
