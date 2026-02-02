namespace WebApi.Infrastructure.Exceptions;

public sealed class ValidationAppException(IReadOnlyList<string> errors) : AppException("Validation failed")
{
    public IReadOnlyList<string> Errors { get; } = errors;
    public override int StatusCode => StatusCodes.Status422UnprocessableEntity;
    public override string Title => "Validation Error";
    public override IReadOnlyList<string>? Details => Errors;

    public ValidationAppException(string error) : this([error])
    {
    }
}
