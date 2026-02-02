
using MediatR;
using WebApi.Application.Features.Employees.DTOs;
using WebApi.Infrastructure.Exceptions;
using WebApi.Infrastructure.Repositories.Contracts;

namespace WebApi.Application.Features.Employees.Queries.Handlers;

public class GetEmployeeByIdQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetEmployeeByIdQuery, GetEmployeeQueryDto>
{
    public async Task<GetEmployeeQueryDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = (await _unitOfWork.EmployeeRepository.GetByIdAsync(request.EmployeeId)).FirstOrDefault();
        if (employee is null)
        {
            throw new NotFoundAppException(nameof(employee), request.EmployeeId);
        }
        return employee;
    }
}
