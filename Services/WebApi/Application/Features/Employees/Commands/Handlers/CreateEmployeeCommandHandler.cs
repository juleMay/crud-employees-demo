using MediatR;
using WebApi.Application.Features.Employees.Commands.Services.Contracts;
using WebApi.Application.Features.Employees.DTOs;

namespace WebApi.Application.Features.Employees.Commands.Handlers;

public class CreateEmployeeCommandHandler(IEmployeeCommandService _employeeCommandService) : IRequestHandler<CreateEmployeeCommand, EmployeeCommandResponseDto>
{
    public async Task<EmployeeCommandResponseDto> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeCommandService.Create(request, cancellationToken);
        return new EmployeeCommandResponseDto
        (
            employee.Id,
            employee.Name,
            employee.Telephone?.ToString(),
            employee.Fax?.ToString(),
            employee.Status,
            employee.CompanyId,
            employee.User.PortalId,
            employee.User.Email.ToString(),
            employee.User.Username,
            string.Empty,
            employee.User.RoleId,
            employee.User.LastLogin,
            employee.CreatedOn,
            employee.UpdatedOn,
            employee.DeletedOn
        );
    }
}
