using System.Linq.Expressions;
using Domain.Common.Constants;
using Domain.Common.Contracts;
using Domain.Common.Exceptions;
using Domain.Common.Resources.SharedResource;
using EFCore.BulkExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Localization;

namespace Infrastructure.Persistence.Repositories.Base;

public class  BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    protected readonly DbSet<TEntity> _dbSet;
    private AppDbContext RepositoryContext { get; set; }
    
    public BaseRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> _localizer)
    {
        this._localizer = _localizer;
        RepositoryContext = repositoryContext;
        _dbSet = repositoryContext.Set<TEntity>();
    }

    public virtual async Task<TEntity> ByIdAsync<TE>(TE id)
    { 
        var entity = await RepositoryContext.Set<TEntity>().FindAsync(id);
        
        if (entity is null)
        {
            throw new NotFoundException(_localizer[ResxKey.NotFoundText, typeof(TE).Name]);
        }

        return entity;
    }
    
    public virtual async Task<TD?> ByIdAsync<TE, TD>(TE id) where TD : class 
        =>  (await RepositoryContext.Set<TEntity>().FindAsync(id))?.Adapt<TD>();
    
    public virtual async Task<TEntity> ByIdsAsync<TE>(params object[] keyValues)
    { 
        var entity = await RepositoryContext.Set<TEntity>().FindAsync(keyValues);
        
        if (entity is null)
        {
            throw new NotFoundException(_localizer[ResxKey.NotFoundText, typeof(TE).Name]);
        }

        return entity;
    }
    
    public virtual async Task<TDest?> ByIdsToAsync<TDest>(params object[] keyValues) where TDest : class 
        =>  (await RepositoryContext.Set<TEntity>().FindAsync(keyValues))?.Adapt<TDest>();

    public async Task<IQueryable<TEntity>> GetAllAsync(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true, bool ignoreQueryFilters = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include(query);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (orderBy != null)
        {
            return orderBy(query);
        }
        else
        {
            return query;
        }
    }

    public async Task<IQueryable<TD>> GetAll<TE, TD>(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true,
        bool ignoreQueryFilters = false) where TD : class
        => (await GetAllAsync(orderBy, include, disableTracking, ignoreQueryFilters)).ProjectToType<TD>();

    /// <summary>
    /// Gets the first entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <remarks>This method default no-tracking query.</remarks>
    public virtual async Task<TDest> FirstToAsync<TDest>(Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = false, bool ignoreQueryFilters = false)
    {
        var entity = await GetFirstOrDefaultAsync<TDest>(predicate, orderBy, include, disableTracking, ignoreQueryFilters);
        if (entity is null)
        {
            throw new NotFoundException(_localizer[ResxKey.NotFoundText, typeof(TEntity).Name]);
        }
        
        return entity;
    }
    
    /// <summary>
    /// Gets the first entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <remarks>This method default no-tracking query.</remarks>
    public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = false, bool ignoreQueryFilters = false)
    {
        var entity = await GetFirstOrDefaultAsync(predicate, orderBy, include, disableTracking, ignoreQueryFilters);
        if (entity is null)
        {
            throw new NotFoundException(_localizer[ResxKey.NotFoundText, typeof(TEntity).Name]);
        }
        
        return entity;
    }

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <remarks>This method default no-tracking query.</remarks>
    public virtual async Task<TDest> GetFirstOrDefaultAsync<TDest>(Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = false, 
        bool ignoreQueryFilters = false)
        => (await GetFirstOrDefaultAsync(predicate, orderBy, include, disableTracking, ignoreQueryFilters)).Adapt<TDest>();
    
    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <remarks>This method default no-tracking query.</remarks>
    public virtual async Task<TEntity> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = false, 
        bool ignoreQueryFilters = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (orderBy != null)
        {
            return await orderBy(query).FirstOrDefaultAsync();
        }
        else
        {
            return await query.FirstOrDefaultAsync();
        }
    }
        
    public IQueryable<TDest> FindByCondition<TDest>(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = false, bool ignoreQueryFilters = false)
        => FindByCondition(predicate, orderBy, include, disableTracking, ignoreQueryFilters).ProjectToType<TDest>();
    
    public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
        bool disableTracking = false, 
        bool ignoreQueryFilters = false)
    {
        IQueryable<TEntity> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (ignoreQueryFilters)
        {
            query = query.IgnoreQueryFilters();
        }

        if (orderBy != null)
        {
            return orderBy(query);
        }
        else
        {
            return query;
        }

    }

    public virtual async Task<TEntity> CreateAsync(TEntity entity, bool save = false)
    {
        await RepositoryContext.Set<TEntity>().AddAsync(entity);
        if (save)
        {
            await SaveAsync();
        }

        return entity;
    }

    public virtual async Task Update(TEntity entity, bool save = false)
    {
        RepositoryContext.Set<TEntity>().Update(entity);
        if (save)
        {
            await SaveAsync();
        }
    }

    public virtual async Task Delete(TEntity entity, bool save = false)
    {
       RepositoryContext.Set<TEntity>().Remove(entity);
       if (save)
       {
           await SaveAsync();
       }
    }
    
    public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
    {
        if (predicate == null)
        {
            return await _dbSet.CountAsync();
        }
        else
        {
            return await _dbSet.CountAsync(predicate);
        }
    }
    
    public virtual async Task<T> MaxAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null)
    {
        if (predicate == null)
            return await _dbSet.MaxAsync(selector);
        else
            return await _dbSet.Where(predicate).MaxAsync(selector);
    }
    
    public virtual async Task<T> MinAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null)
    {
        if (predicate == null)
            return await _dbSet.MinAsync(selector);
        else
            return await _dbSet.Where(predicate).MinAsync(selector);
    }
    
    public virtual async Task<decimal> AverageAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null)
    {
        if (predicate == null)
            return await _dbSet.AverageAsync(selector);
        else
            return await _dbSet.Where(predicate).AverageAsync(selector);
    }
    
    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> selector = null)
    {
        if (selector == null)
        {
            return await _dbSet.AnyAsync();
        }
        else
        {
            return await _dbSet.AnyAsync(selector);
        }
    }
    
    public virtual async Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null)
    {
        if (predicate == null)
            return await _dbSet.SumAsync(selector);
        else
            return await _dbSet.Where(predicate).SumAsync(selector);
    }

    public virtual async Task BulkUpdateAsync(IEnumerable<TEntity> entities) => await RepositoryContext.Set<TEntity>().BatchUpdateAsync(entities);

    public virtual async Task BulkInsertAsync(IList<TEntity> entities) => await RepositoryContext.BulkInsertAsync(entities, 
        new BulkConfig()
        {
            SetOutputIdentity = true
        });
    
    public virtual async Task BulkDeleteAsync(IList<TEntity> entities) => await RepositoryContext.BulkDeleteAsync(entities);
    
    public virtual async Task BulkUpsert(IList<TEntity> entities) => await RepositoryContext.BulkInsertOrUpdateAsync(entities,  
        new BulkConfig()
        {
            SetOutputIdentity = true
        });
    
    public virtual async Task BulkUpsertOrDelete(IList<TEntity> entities) => await RepositoryContext.BulkInsertOrUpdateOrDeleteAsync(entities,
        new BulkConfig()
        {
            SetOutputIdentity = true
        });

    public virtual async Task SaveAsync() => await RepositoryContext.SaveChangesAsync();
    
    public virtual async Task BulkSaveAsync() => await RepositoryContext.BulkSaveChangesAsync();

}