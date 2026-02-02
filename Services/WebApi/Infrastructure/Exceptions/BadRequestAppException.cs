namespace WebApi.Infrastructure.Exceptions;

public sealed class BadRequestAppException(string message) : AppException(message)
{
    public override int StatusCode => StatusCodes.Status400BadRequest;
    public override string Title => "Bad Request";
}
