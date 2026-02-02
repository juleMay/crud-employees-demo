namespace WebApi.Domain.Contracts;

public interface IAuditableDto
{
    DateTime? CreatedOn { get; set; }
    DateTime? UpdatedOn { get; set; }
    DateTime? DeletedOn { get; set; }
}