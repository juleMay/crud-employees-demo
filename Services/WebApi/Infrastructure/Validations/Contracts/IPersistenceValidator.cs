namespace WebApi.Infrastructure.Validations.Contracts;

public interface IPersistenceValidator<in TRequest>
{
    Task ValidateAsync(TRequest request, CancellationToken ct);
}
