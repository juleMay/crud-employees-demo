using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Repositories.Contracts;

public interface IUnitOfWork : IDisposable
{
    IEmployeeRepository EmployeeRepository { get; }
    IUserRepository UserRepository { get; }
    IGenericRepository<Role> RoleGenericRepository { get; }
    IGenericRepository<Portal> PortalGenericRepository { get; }
    IGenericRepository<Company> CompanyGenericRepository { get; }
    void Save();
    Task SaveAsync(CancellationToken cancellationToken = default);
}