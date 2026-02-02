using MediatR;
using WebApi.Application.Features.Employees.Commands.Services.Contracts;

namespace WebApi.Application.Features.Employees.Commands.Handlers;

public class UpdateEmployeeCommandHandler(IEmployeeCommandService _employeeCommandService) : IRequestHandler<UpdateEmployeeCommand>
{
    public async Task Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _employeeCommandService.Update(request, cancellationToken);
    }
}
