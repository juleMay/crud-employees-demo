using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;
using WebApi.Infrastructure.Contexts;
using WebApi.Infrastructure.Repositories.Contracts;

namespace WebApi.Infrastructure.Repositories;

public class UserRepository(ReadDbContext readDbContext, WriteDbContext writeDbContext) : GenericRepository<User>(readDbContext, writeDbContext), IUserRepository
{
    public Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default)
    {
        return _readDbSet.AnyAsync(u => email.Equals(u.Email), cancellationToken);
    }

    public Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return _readDbSet.AnyAsync(u => u.Username.Equals(userName), cancellationToken);
    }

    public Task<bool> ExistsByEmailAsync(Email email, Guid userId, CancellationToken cancellationToken = default)
    {
        return _readDbSet.AnyAsync(u => u.Email.Equals(email) && u.Id != userId, cancellationToken);
    }

    public Task<bool> ExistsByUserNameAsync(string userName, Guid userId, CancellationToken cancellationToken = default)
    {
        return _readDbSet.AnyAsync(u => u.Username.Equals(userName) && u.Id != userId, cancellationToken);
    }
}