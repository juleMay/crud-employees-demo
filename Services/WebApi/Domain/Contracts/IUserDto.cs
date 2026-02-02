namespace WebApi.Domain.Contracts;

public interface IUserDto
{
    Guid PortalId { get; set; }
    string Email { get; set; }
    string Username { get; set; }
    string Password { get; set; }
    Guid RoleId { get; set; }
    DateTime? LastLogin { get; set; }
}
