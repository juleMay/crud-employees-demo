using WebApi.Domain.Primitives;

namespace WebApi.Domain.Entities;

public class Company(Guid id, string name) : Entity<Guid>(id)
{
    public string Name { get; private set; } = name;
    public IReadOnlyCollection<Employee> Employees => _employees;
    private readonly List<Employee> _employees = [];

    public void HireEmployee(Employee employee)
    {
        if (employee == null)
        {
            throw new ArgumentNullException(nameof(employee));
        }

        _employees.Add(employee);
    }
}
