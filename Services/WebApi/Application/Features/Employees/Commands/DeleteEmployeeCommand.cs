using MediatR;

namespace WebApi.Application.Features.Employees.Commands;

public class DeleteEmployeeCommand(Guid employeeId) : IRequest
{
    public Guid EmployeeId {get; set;} = employeeId;
}
