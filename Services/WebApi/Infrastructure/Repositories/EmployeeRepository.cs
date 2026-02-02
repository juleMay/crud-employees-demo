using Dapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.Features.Employees.DTOs;
using WebApi.Domain.Entities;
using WebApi.Infrastructure.Contexts;
using WebApi.Infrastructure.Pagination;
using WebApi.Infrastructure.Pagination.DTOs;
using WebApi.Infrastructure.Repositories.Contracts;

namespace WebApi.Infrastructure.Repositories;

public class EmployeeRepository(ReadDbContext readDbContext, WriteDbContext writeDbContext) : GenericRepository<Employee>(readDbContext, writeDbContext), IEmployeeRepository
{
    private readonly ReadDbContext _readDbContext = readDbContext;

    public async Task<Employee?> GetIncludeUserByIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        return await _readDbSet.Include(e => e.User).FirstOrDefaultAsync(e => employeeId.Equals(e.Id), cancellationToken);
    }

    public async Task<IEnumerable<GetEmployeeQueryDto>> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        using var connection = _readDbContext.GetMySqlConnection();
        await connection.OpenAsync(cancellationToken);
        string queries = @"SELECT
                            e.EmployeeId        AS EmployeeId,
                            e.Name              AS Name,
                            e.TelephoneNumber   AS Telephone,
                            e.FaxNumber         AS Fax,
                            e.Status            AS StatusId,
                            e.CreatedOn         AS CreatedOn,
                            e.UpdatedOn         AS UpdatedOn,
                            e.DeletedOn         AS DeletedOn,
                            u.UserId            AS UserId,
                            u.Username          AS Username,
                            u.Email             AS Email,
                            u.LastLogin         AS LastLogin,
                            u.RoleId            AS RoleId,
                            r.Name              AS RoleName,
                            u.PortalId          AS PortalId,
                            p.Name              AS PortalName,
                            e.CompanyId         AS CompanyId,
                            c.Name              AS CompanyName
                        FROM Employee e
                        INNER JOIN User u ON u.UserId = e.UserId
                        INNER JOIN Role r ON r.RoleId = u.RoleId
                        INNER JOIN Portal p ON p.PortalId = u.PortalId
                        INNER JOIN Company c ON c.CompanyId = e.CompanyId
                        WHERE e.DeletedOn IS NULL AND e.EmployeeId = @EmployeeId
                        ORDER BY e.EmployeeId;";

        return await connection.QueryAsync<GetEmployeeQueryDto>(queries,
            new
            {
                EmployeeId = employeeId
            });
    }

    public async Task<PagedList<GetEmployeeQueryDto>> GetAsync(PagedRequestDto pagedRequest, CancellationToken cancellationToken = default)
    {
        using var connection = _readDbContext.GetMySqlConnection();
        await connection.OpenAsync(cancellationToken);
        string queries = @"SELECT
                            e.EmployeeId        AS EmployeeId,
                            e.Name              AS Name,
                            e.TelephoneNumber   AS Telephone,
                            e.FaxNumber         AS Fax,
                            e.Status            AS StatusId,
                            e.CreatedOn         AS CreatedOn,
                            e.UpdatedOn         AS UpdatedOn,
                            e.DeletedOn         AS DeletedOn,
                            u.UserId            AS UserId,
                            u.Username          AS Username,
                            u.Email             AS Email,
                            u.LastLogin         AS LastLogin,
                            u.RoleId            AS RoleId,
                            r.Name              AS RoleName,
                            u.PortalId          AS PortalId,
                            p.Name              AS PortalName,
                            e.CompanyId         AS CompanyId,
                            c.Name              AS CompanyName
                        FROM Employee e
                        INNER JOIN User u ON u.UserId = e.UserId
                        INNER JOIN Role r ON r.RoleId = u.RoleId
                        INNER JOIN Portal p ON p.PortalId = u.PortalId
                        INNER JOIN Company c ON c.CompanyId = e.CompanyId
                        WHERE e.DeletedOn IS NULL
                        ORDER BY e.EmployeeId
                        LIMIT @PageSize OFFSET @Offset;
                        SELECT COUNT(*) AS TotalItems FROM Employee e WHERE e.DeletedOn IS NULL;";

        using var multiQuery = await connection.QueryMultipleAsync(queries,
            new
            {
                Offset = (pagedRequest.Page - 1) * pagedRequest.PageSize,
                pagedRequest.PageSize
            });

        var items = await multiQuery.ReadAsync<GetEmployeeQueryDto>().ConfigureAwait(false);

        var totalItems = await multiQuery.ReadFirstAsync<int>().ConfigureAwait(false);

        return new PagedList<GetEmployeeQueryDto>([.. items], pagedRequest.Page, pagedRequest.PageSize, totalItems);
    }
}
