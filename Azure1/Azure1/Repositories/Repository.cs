using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Azure1.Data;

namespace Azure1.Repositories;


public class Repository<TEntity> where TEntity : class
{
    internal AzureDbContext context;
    internal DbSet<TEntity> dbSet;

    public Repository(AzureDbContext inContext)
    {
        context = inContext;
        dbSet = context.Set<TEntity>();
    }

    public virtual IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
    {
        IQueryable<TEntity> query = dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return orderBy != null
            ? orderBy(query)
            : query;
    }

    public virtual IQueryable<TEntity> GetQueryAsNoTracking(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "")
    {
        return GetQuery(filter, orderBy, includeProperties).AsNoTracking();
    }

    public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
    {
        return GetQuery(filter, orderBy, includeProperties).ToList();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
    {
        return await GetQuery(filter, orderBy, includeProperties).ToListAsync();
    }

    public virtual async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
    {
        return await GetQuery(filter, orderBy, includeProperties).FirstOrDefaultAsync();
    }

    public virtual TEntity? GetById(object id)
    {
        return dbSet.Find(id);
    }

    public virtual void Insert(TEntity entity)
    {
        dbSet.Add(entity);
    }

    public virtual void Delete(object id)
    {
        TEntity? entityToDelete = dbSet.Find(id);
        if (entityToDelete == null) return;
        Delete(entityToDelete);
    }

    public virtual void Delete(TEntity entityToDelete)
    {
        if (context.Entry(entityToDelete).State == EntityState.Detached)
        {
            dbSet.Attach(entityToDelete);
        }
        dbSet.Remove(entityToDelete);
    }

    public virtual void Update(TEntity entityToUpdate)
    {
        dbSet.Attach(entityToUpdate);
        context.Entry(entityToUpdate).State = EntityState.Modified;
    }
}

