using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure.Contexts;
using WebApi.Infrastructure.Repositories.Contracts;

namespace WebApi.Infrastructure.Repositories;

public class GenericRepository<TEntity>(ReadDbContext readDbContext, WriteDbContext writeDbContext) : IGenericRepository<TEntity> where TEntity : class
{
    protected DbSet<TEntity> _readDbSet = readDbContext.Set<TEntity>();
    protected DbSet<TEntity> _writeDbSet = writeDbContext.Set<TEntity>();

    public virtual IEnumerable<TEntity> Get(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<TEntity> query = _readDbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    public virtual TEntity? GetById(object id)
    {
        return _readDbSet.Find(id);
    }

    public virtual async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
    {
        return await _readDbSet.FindAsync([id, cancellationToken], cancellationToken: cancellationToken);
    }

    public virtual void Insert(TEntity entity)
    {
        _writeDbSet.Add(entity);
    }

    public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _writeDbSet.AddAsync(entity, cancellationToken);
    }

    public virtual void Delete(object id)
    {
        TEntity? entityToDelete = _writeDbSet.Find(id);
        if (entityToDelete != null)
        {
            Delete(entityToDelete);
        }
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (_writeDbSet.Entry(entityToDelete).State == EntityState.Detached)
        {
            _writeDbSet.Attach(entityToDelete);
        }
        _writeDbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        _writeDbSet.Attach(entityToUpdate);
        _writeDbSet.Entry(entityToUpdate).State = EntityState.Modified;
    }
}