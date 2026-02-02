using MediatR;
using WebApi.Application.Features.Employees.DTOs;

namespace WebApi.Application.Features.Employees.Commands;

public record struct UpdateEmployeeCommand : IRequest
{
    public Guid EmployeeId { get; set; }
    public EmployeeDto EmployeeDto { get; set; }

    public UpdateEmployeeCommand(Guid employeeId, EmployeeDto employeeDto)
    {
        EmployeeId = employeeId;
        EmployeeDto = employeeDto;
    }
}
