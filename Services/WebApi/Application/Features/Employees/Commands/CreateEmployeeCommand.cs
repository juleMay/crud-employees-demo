using MediatR;
using WebApi.Application.Features.Employees.DTOs;

namespace WebApi.Application.Features.Employees.Commands;

public record struct CreateEmployeeCommand : IRequest<EmployeeCommandResponseDto>
{
    public EmployeeDto EmployeeDto { get; set; }

    public CreateEmployeeCommand(EmployeeDto employeeDto)
    {
        EmployeeDto = employeeDto;
    }
}