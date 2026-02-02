using WebApi.Domain.Enums;

namespace WebApi.Application.Features.Employees.DTOs;

public class EmployeeCommandResponseDto(Guid employeeId, string? name, string? telephone, string? fax, EmployeeStatus statusId, Guid companyId, Guid portalId, string email, string username, string password, Guid roleId, DateTime? lastlogin, DateTime? createdOn, DateTime? updatedOn, DateTime? deletedOn) : EmployeeDto(name, telephone, fax, statusId, companyId, portalId, email, username, password, roleId, lastlogin, createdOn, updatedOn, deletedOn)
{
    public Guid EmployeeId { get; set; } = employeeId;
}
