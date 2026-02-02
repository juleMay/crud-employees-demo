namespace WebApi.Domain.Contracts;

public interface IEmployeeCommand
{
    IEmployeeDto EmployeeDto { get; set; }
    IUserDto UserDto { get; set; }
}
