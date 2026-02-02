namespace WebApi.Infrastructure.Middlewares.DTOs;

public sealed class AppErrorDto
{
    public string? Title { get; init; }
    public string Message { get; init; } = default!;
    public int? Status { get; init; }
    public IReadOnlyList<string>? Details { get; init; }
}
