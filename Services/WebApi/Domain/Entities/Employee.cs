using WebApi.Domain.Contracts;
using WebApi.Domain.Enums;
using WebApi.Domain.Primitives;
using WebApi.Domain.ValueObjects;

namespace WebApi.Domain.Entities;

public class Employee : AuditableEntity<Guid>
{
    public string? Name { get; private set; }
    public PhoneNumber? Telephone { get; private set; }
    public PhoneNumber? Fax { get; private set; }
    public EmployeeStatus Status { get; private set; }
    public Guid UserId { get; }
    public User User { get; private set; }
    public Guid CompanyId { get; }
    public Company Company { get; private set; }

    private Employee() : base(Guid.Empty) { }
    private Employee(
        Guid id,
        User user,
        string? name,
        PhoneNumber? telephone,
        PhoneNumber? fax,
        EmployeeStatus status,
        DateTime createdOn
    ) : base(id)
    {
        User = user;
        Name = name;
        Telephone = telephone;
        Fax = fax;
        Status = status;
        Company = null!;
        CreatedOn = createdOn;
    }

    public static Employee Create(IEmployeeDto employeeDto, User user)
    {
        return new Employee(
            Guid.NewGuid(),
            user,
            employeeDto.Name,
            employeeDto.Telephone == null ? null : PhoneNumber.Create(employeeDto.Telephone),
            employeeDto.Fax == null ? null : PhoneNumber.Create(employeeDto.Fax),
            employeeDto.StatusId,
            DateTime.UtcNow
        );
    }

    public void Update(IEmployeeDto employeeDto)
    {
        Name = employeeDto.Name;
        Telephone = employeeDto.Telephone == null ? null : PhoneNumber.Create(employeeDto.Telephone);
        Fax = employeeDto.Fax == null ? null : PhoneNumber.Create(employeeDto.Fax);
        UpdatedOn = employeeDto.UpdatedOn ?? DateTime.UtcNow;
    }

    public void Reinstate()
    {
        Status = EmployeeStatus.Reinstated;
        DeletedOn = null;
    }

    public void Dismiss(DateTime? deletedOn = null)
    {
        Status = EmployeeStatus.Inactive;
        DeletedOn = deletedOn ?? DateTime.UtcNow;
    }

    public void Fire(DateTime? deletedOn = null)
    {
        Status = EmployeeStatus.Inactive;
        DeletedOn = deletedOn;
    }
}
