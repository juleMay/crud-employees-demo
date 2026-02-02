using MediatR;
using WebApi.Infrastructure.Repositories.Contracts;

namespace WebApi.Application.Features.Employees.Commands.Handlers;

public class DeleteEmployeeCommandHandler(IUnitOfWork _unitOfWork) : IRequestHandler<DeleteEmployeeCommand>
{
    public Task Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = _unitOfWork.EmployeeRepository.GetById(request.EmployeeId);
        if (employee is not null)
        {
            employee.Dismiss();
            _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.SaveAsync(cancellationToken);
        }
        return Task.CompletedTask;
    }
}
