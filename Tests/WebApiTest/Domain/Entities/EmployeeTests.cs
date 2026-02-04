using FluentAssertions;
using WebApi.Domain.Entities;
using WebApi.Domain.Enums;
using WebApiTest.Domain.Builders;

namespace WebApiTest.Domain.Entities;

public class EmployeeTests
{
    [Fact]
    public void CreateEmployee_ValidEmployeeDto_Should_CreateEmployee()
    {
        #region Arrange
        var employeeDto = new EmployeeDtoBuilder().Build();
        #endregion
        #region Act
        var user = User.Create(employeeDto);
        var employee = Employee.Create(employeeDto, user);
        #endregion
        #region Assert
        employee.Name.Should().Be(employeeDto.Name);
        employee.Telephone?.Number.Should().Be(employeeDto.Telephone);
        employee.Fax?.Number.Should().Be(employeeDto.Fax);
        employee.Status.Should().Be(employeeDto.StatusId);
        employee.User.Should().Be(user);
        employee.User.Username.Should().Be(employeeDto.Username);
        employee.User.Email.Address.Should().Be(employeeDto.Email);
        employee.User.LastLogin.Should().Be(employeeDto.LastLogin);
        #endregion
    }

    [Fact]
    public void UpdateEmployee_ValidEmployeeDto_Should_UpdateEmployee()
    {
        #region Arrange
        var employeeDto = new EmployeeDtoBuilder().Build();
        var user = User.Create(employeeDto);
        var employee = Employee.Create(employeeDto, user);
        var name = "Updated";
        var telephone = "100000000";
        var fax = "200000000";
        var updatedOn = DateTime.UtcNow; ;
        employeeDto = new EmployeeDtoBuilder()
                        .WithName(name)
                        .WithTelephone(telephone)
                        .WithFax(fax)
                        .WithUpdatedOn(updatedOn)
                        .Build();
        #endregion
        #region Act
        employee.Update(employeeDto);
        #endregion
        #region Assert
        employee.Name.Should().Be(name);
        employee.Telephone?.Number.Should().Be(telephone);
        employee.Fax?.Number.Should().Be(fax);
        employee.UpdatedOn?.Should().Be(updatedOn);
        #endregion
    }

    [Fact]
    public void UpdateEmployee_DismissEmployee_Should_SetStatusToInactive()
    {
        #region Arrange
        var employeeDto = new EmployeeDtoBuilder().Build();
        #endregion
        #region Act
        var user = User.Create(employeeDto);
        var employee = Employee.Create(employeeDto, user);
        employee.Dismiss();
        #endregion
        #region Assert
        employee.Status.Should().Be(EmployeeStatus.Inactive);
        #endregion
    }
}