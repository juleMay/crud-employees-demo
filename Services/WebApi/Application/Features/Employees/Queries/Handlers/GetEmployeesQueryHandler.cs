using MediatR;
using WebApi.Application.Features.Employees.DTOs;
using WebApi.Infrastructure.Pagination;
using WebApi.Infrastructure.Repositories.Contracts;

namespace WebApi.Application.Features.Employees.Queries.Handlers;

public class GetEmployeesQueryHandler(IUnitOfWork _unitOfWork) : IRequestHandler<GetEmployeesQuery, PagedList<GetEmployeeQueryDto>>
{
    public async Task<PagedList<GetEmployeeQueryDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.EmployeeRepository.GetAsync(request.PagedRequest);
    }
}