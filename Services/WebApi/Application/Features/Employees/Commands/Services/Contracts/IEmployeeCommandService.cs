using WebApi.Domain.Entities;

namespace WebApi.Application.Features.Employees.Commands.Services.Contracts;

public interface IEmployeeCommandService
{
    Task<Employee> Create(CreateEmployeeCommand request, CancellationToken cancellationToken);
    Task Update(UpdateEmployeeCommand request, CancellationToken cancellationToken);
}
