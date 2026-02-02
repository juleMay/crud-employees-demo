using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Repositories.Contracts;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByUserNameAsync(string userName, Guid userId, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, Guid userId, CancellationToken cancellationToken = default);
}
