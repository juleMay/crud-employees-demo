namespace WebApi.Application.Features.Employees.DTOs;

public class GetEmployeeQueryDto
{
    public Guid EmployeeId { get; set; }
    public string? Name { get; set; }
    public string? Telephone { get; set; }
    public string? Fax { get; set; }
    public int StatusId { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }
    public  Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime? LastLogin { get; set; }
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public Guid PortalId { get; set; }
    public string PortalName { get; set; } = string.Empty;
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set; } = string.Empty;
}