using WebApi.Domain.Enums;

namespace WebApi.Domain.Contracts;

public interface IEmployeeDto: IAuditableDto
{
    string? Name { get; set; }
    string? Fax { get; set; }
    string? Telephone { get; set; }
    EmployeeStatus StatusId { get; set; }
    Guid CompanyId { get; set; }
}
