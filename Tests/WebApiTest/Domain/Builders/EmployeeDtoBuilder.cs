using WebApi.Application.Features.Employees.DTOs;
using WebApi.Domain.Enums;

namespace WebApiTest.Domain.Builders;

public class EmployeeDtoBuilder
{
    private string _name = "employee";
    private string _telephone = "3121234567";
    private string _fax = "8201212";
    private EmployeeStatus _statusId = EmployeeStatus.Active;
    private Guid _companyId = Guid.Parse("8955fec5-fe34-11f0-b5cc-ca2631db2dcd");
    private DateTime createdOn = DateTime.UtcNow;
    private DateTime? updatedOn = null;
    private DateTime? deletedOn = null;
    private Guid _portalId = Guid.Parse("b5b8c2b4-fe34-11f0-b5cc-ca2631db2dcd");
    private string _email = "employee@yopmail.com";
    private string _username = "employee";
    private string _password = "password123";
    private Guid _roleId = Guid.Parse("c6a22cb9-fe34-11f0-b5cc-ca2631db2dcd");
    private DateTime? _lastlogin = null;

    public EmployeeDtoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    public EmployeeDtoBuilder WithTelephone(string telephone)
    {
        _telephone = telephone;
        return this;
    }
    public EmployeeDtoBuilder WithFax(string fax)
    {
        _fax = fax;
        return this;
    }

    public EmployeeDtoBuilder WithStatusId(EmployeeStatus statusId)
    {
        _statusId = statusId;
        return this;
    }

    public EmployeeDtoBuilder WithCompanyId(Guid companyId)
    {
        _companyId = companyId;
        return this;
    }

    public EmployeeDtoBuilder WithCreatedOn(DateTime createdOn)
    {
        this.createdOn = createdOn;
        return this;
    }

    public EmployeeDtoBuilder WithUpdatedOn(DateTime updatedOn)
    {
        this.updatedOn = updatedOn;
        return this;
    }
    public EmployeeDtoBuilder WithDeletedOn(DateTime deletedOn)
    {
        this.deletedOn = deletedOn;
        return this;
    }

    public EmployeeDtoBuilder WithPortalId(Guid portalId)
    {
        _portalId = portalId;
        return this;
    }

    public EmployeeDtoBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public EmployeeDtoBuilder WithUsername(string username)
    {
        _username = username;
        return this;
    }

    public EmployeeDtoBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }

    public EmployeeDtoBuilder WithRoleId(Guid roleId)
    {
        _roleId = roleId;
        return this;
    }

    public EmployeeDtoBuilder WithLastLogin(DateTime? lastLogin)
    {
        _lastlogin = lastLogin;
        return this;
    }

    public CreateEmployeeDto Build()
    {
        return new CreateEmployeeDto
        (
            _name,
            _telephone,
            _fax,
            _statusId,
            _companyId,
            _portalId,
            _email,
            _username,
            _password,
            _roleId,
            _lastlogin,
            createdOn,
            updatedOn,
            deletedOn
        );
    }
}