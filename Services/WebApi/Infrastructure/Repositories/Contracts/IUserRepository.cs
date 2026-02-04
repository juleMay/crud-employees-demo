using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Infrastructure.Repositories.Contracts;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByUserNameAsync(string userName, Guid userId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(Email email, Guid userId, CancellationToken cancellationToken = default);
}
