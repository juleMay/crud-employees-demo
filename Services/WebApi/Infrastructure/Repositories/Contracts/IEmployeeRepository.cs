using WebApi.Application.Features.Employees.DTOs;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Pagination;
using WebApi.Infrastructure.Pagination.DTOs;

namespace WebApi.Infrastructure.Repositories.Contracts;

public interface IEmployeeRepository : IGenericRepository<Employee>
{
    Task<Employee?> GetIncludeUserByIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<GetEmployeeQueryDto>> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task<PagedList<GetEmployeeQueryDto>> GetAsync(PagedRequestDto pagedRequest, CancellationToken cancellationToken = default);
}
