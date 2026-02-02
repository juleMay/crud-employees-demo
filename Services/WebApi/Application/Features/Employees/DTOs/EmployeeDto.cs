using WebApi.Domain.Contracts;
using WebApi.Domain.Enums;

namespace WebApi.Application.Features.Employees.DTOs;

public abstract class EmployeeDto : IEmployeeDto, IUserDto
{
    public string? Name { get; set; }
    public string? Telephone { get; set; }
    public string? Fax { get; set; }
    public EmployeeStatus StatusId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid PortalId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid RoleId { get; set; }
    public DateTime? LastLogin { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }

    public EmployeeDto() { }

    public EmployeeDto(string? name, string? telephone, string? fax, EmployeeStatus statusId, Guid companyId, Guid portalId, string email, string username, string password, Guid roleId, DateTime? lastlogin, DateTime? createdOn, DateTime? updatedOn, DateTime? deletedOn)
    {
        Name = name;
        Telephone = telephone;
        Fax = fax;
        StatusId = statusId;
        CompanyId = companyId;
        PortalId = portalId;
        Email = email;
        Username = username;
        Password = password;
        RoleId = roleId;
        LastLogin = lastlogin;
        CreatedOn = createdOn;
        UpdatedOn = updatedOn;
        DeletedOn = deletedOn;
    }
}
