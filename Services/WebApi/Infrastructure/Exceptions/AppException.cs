namespace WebApi.Infrastructure.Exceptions;

public abstract class AppException(string message) : Exception(message)
{
    public abstract int StatusCode { get; }
    public virtual string? Title => null;
    public virtual IReadOnlyList<string>? Details => null;
}
