using MediatR;
using WebApi.Application.Features.Employees.DTOs;
using WebApi.Infrastructure.Pagination;
using WebApi.Infrastructure.Pagination.DTOs;

namespace WebApi.Application.Features.Employees.Queries;

public class GetEmployeesQuery(PagedRequestDto pagedRequest) : IRequest<PagedList<GetEmployeeQueryDto>>
{
    public PagedRequestDto PagedRequest { get; set; } = pagedRequest;
}