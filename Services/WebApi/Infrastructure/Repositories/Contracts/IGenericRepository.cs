using System.Linq.Expressions;

namespace WebApi.Infrastructure.Repositories.Contracts;

public interface IGenericRepository<TEntity> where TEntity : class
{
    IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");
    TEntity? GetById(object id);
    Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);
    void Insert(TEntity entity);
    Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Delete(object id);
    void Delete(TEntity entityToDelete);
    void Update(TEntity entityToUpdate);
}
