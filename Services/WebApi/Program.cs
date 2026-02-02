using Carter;
using DotNetEnv;
using Microsoft.OpenApi.Models;
using Spectre.Console;
using WebApi.Infrastructure.Dependencies;
using WebApi.Infrastructure.Middlewares;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

if (environment.Equals("Development"))
{
    Env.Load();
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minimal API Demo",
        Version = "v1",
        Description = ".NET Core 8 Minimal API with Swagger"
    });
});

builder.Services.AddHealthChecks();

builder.Services.AddServices(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<AppErrorMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API Demo v1");
    });
}

app.UseHttpsRedirection();

app.MapHealthChecks("/healthz");
app.MapControllers();

app.MapCarter();

app.UseCors(builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

AnsiConsole.MarkupLine("[green]✓ Build completed successfully[/]");
try
{
    AnsiConsole.MarkupLine("[#FFA500]⚠[/] [yellow]Starting application[/]");
    await app.RunAsync();
}
catch (Exception e)
{
    AnsiConsole.MarkupLine("[red]✗ Application start failed[/]");
    AnsiConsole.MarkupLine($"[red]{e.Message}[/]");
}
