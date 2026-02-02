using WebApi.Infrastructure.Exceptions;
using WebApi.Infrastructure.Middlewares.DTOs;

namespace WebApi.Infrastructure.Middlewares;

public sealed class AppErrorMiddleware(
    RequestDelegate next,
    ILogger<AppErrorMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<AppErrorMiddleware> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            await WriteAppError(context, ex);
        }
        catch (BadHttpRequestException ex)
        {
            await WriteAppError(context, new BadRequestAppException(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            await WriteAppError(context, new AppErrorDto
            {
                Title = "Unexpected Error",
                Message = "An unexpected error occurred",
                Status = StatusCodes.Status500InternalServerError
            });
        }
    }

    private static async Task WriteAppError(
        HttpContext context,
        AppException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex.StatusCode;

        var error = new AppErrorDto
        {
            Title = ex.Title,
            Message = ex.Message,
            Status = ex.StatusCode,
            Details = ex.Details
        };

        await context.Response.WriteAsJsonAsync(error);
    }

    private static async Task WriteAppError(
        HttpContext context,
        AppErrorDto error)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = error.Status ?? 500;

        await context.Response.WriteAsJsonAsync(error);
    }
}
