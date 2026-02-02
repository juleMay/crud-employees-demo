namespace WebApi.Domain.Primitives;

public abstract class AuditableEntity<TId>(TId id) : Entity<TId>(id)
{
    public DateTime CreatedOn { get; protected set; } = DateTime.UtcNow;
    public DateTime? UpdatedOn { get; protected set; }
    public DateTime? DeletedOn { get; protected set; }
}
