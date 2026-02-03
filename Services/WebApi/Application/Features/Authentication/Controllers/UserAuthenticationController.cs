using Microsoft.AspNetCore.Mvc;
using WebApi.Domain.Entities.Authentication;

namespace WebApi.Application.Features.Users.Controllers;

[ApiController]
[Route("v1/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> GetToken([FromForm] LoginInfo loginInfo, [FromServices] IConfiguration configuration)
    {
        string uri = configuration.GetValue<string>("Auth:GetSecureTokenEndpoint") ?? string.Empty;
        using HttpClient client = new();
        FireBaseLoginInfo fireBaseLoginInfo = new()
        {
            email = loginInfo.Username,
            password = loginInfo.Password
        };
        var result = await client.PostAsJsonAsync(uri, fireBaseLoginInfo);
        if (!result.IsSuccessStatusCode)
        {
            return Unauthorized();
        }
        var encoded = await result.Content.ReadFromJsonAsync<GoogleToken>();
        Token token = new()
        {
            token_type = "Bearer",
            access_token = encoded.idToken,
            id_token = encoded.idToken,
            expires_in = int.Parse(encoded.expiresIn),
            refresh_token = encoded.refreshToken
        };
        return Ok(token);
    }
}
