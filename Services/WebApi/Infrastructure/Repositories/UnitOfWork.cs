using WebApi.Domain.Entities;
using WebApi.Infrastructure.Contexts;
using WebApi.Infrastructure.Repositories.Contracts;

namespace WebApi.Infrastructure.Repositories;

public class UnitOfWork(WriteDbContext writeContext, IEmployeeRepository employeeRepository, IUserRepository userRepository, IGenericRepository<Role> roleGenericRepository, IGenericRepository<Portal> portalGenericRepository, IGenericRepository<Company> companyGenericRepository) : IUnitOfWork
{
    private readonly WriteDbContext _writeContext = writeContext;
    private bool disposed = false;


    public IEmployeeRepository EmployeeRepository { get; } = employeeRepository;

    public IUserRepository UserRepository => userRepository;

    public IGenericRepository<Role> RoleGenericRepository => roleGenericRepository;

    public IGenericRepository<Portal> PortalGenericRepository => portalGenericRepository;

    public IGenericRepository<Company> CompanyGenericRepository => companyGenericRepository;

    public void Save()
    {
        _writeContext.SaveChanges();
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _writeContext.SaveChangesAsync(cancellationToken);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            _writeContext.Dispose();
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}