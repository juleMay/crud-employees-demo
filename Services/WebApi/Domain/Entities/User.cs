using WebApi.Domain.Contracts;
using WebApi.Domain.Primitives;
using WebApi.Domain.ValueObjects;
using WebApi.Infrastructure.Exceptions;

namespace WebApi.Domain.Entities;

public class User : Entity<Guid>
{
    public string Username { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public Guid RoleId { get; }
    public Role Role { get; private set; }
    public DateTime? LastLogin { get; private set; }
    public Employee Employee { get; private set; }
    public Guid PortalId { get; }
    public Portal Portal { get; private set; }

    public User() : base(Guid.Empty) { }
    public User(
        Guid id,
        string username,
        Email email,
        Password password
    ) : base(id)
    {
        Username = username;
        Email = email;
        Password = password;
        Portal = null!;
        Employee = null!;
        Role = null!;
    }

    public static User Create(IUserDto userDto)
    {
        if (userDto == null)
        {
            throw new ValidationAppException(nameof(userDto));
        }

        var email = Email.Create(userDto.Email);
        var password = Password.Create(userDto.Password);

        var user = new User(
            Guid.NewGuid(),
            userDto.Username,
            email,
            password
        );

        return user;
    }

    public void Update(IUserDto userDto)
    {

    }

    public void Login()
    {
        LastLogin = DateTime.UtcNow;
    }

    public void ChangePassword(Password newPassword)
    {
        Password = newPassword;
    }
}
