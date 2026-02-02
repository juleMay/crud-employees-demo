namespace WebApi.Infrastructure.Exceptions;

public sealed class NotFoundAppException(string entity, object key) : AppException($"Item from Entity: '{entity}', with Key: '{key}' not found")
{
    public string Entity { get; } = entity;
    public object Key { get; } = key;

    public override int StatusCode => StatusCodes.Status404NotFound;
    public override string Title => "Not Found";
}