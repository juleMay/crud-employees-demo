using System.Reflection;
using Carter;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Options;
using Spectre.Console;
using WebApi.Application.Features.Employees.Commands.Services;
using WebApi.Application.Features.Employees.Commands.Services.Contracts;
using WebApi.Infrastructure.Contexts;
using WebApi.Infrastructure.Options;
using WebApi.Infrastructure.Repositories;
using WebApi.Infrastructure.Repositories.Contracts;
using WebApi.Infrastructure.Validations;

namespace WebApi.Infrastructure.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddCarter();
        var conectionString = configuration.GetSection(AppSettings.SectionName).Value ?? "N/A";
        AnsiConsole.MarkupLine($"[#FFA500]⚠[/] [yellow]Conecting to DataBase: {conectionString}[/]");
        services.Configure<AppSettings>(appSettings =>
        {
            appSettings.ConectionString = conectionString;
        });
        services.Configure<AppSettings>(configuration.GetSection(AppSettings.SectionName));
        services.AddDbContext<WriteDbContext>((serviceProvider, options) =>
        {
            var appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
            DbContextConfiguration.WriteOptions(options, appSettings.ConectionString);
        });
        services.AddDbContext<ReadDbContext>((serviceProvider, options) =>
        {
            var appSettings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value;
            DbContextConfiguration.ReadOptions(options, appSettings.ConectionString);
        });
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEmployeeCommandService, EmployeeCommandService>();
        services.AddFluentValidationAutoValidation();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
