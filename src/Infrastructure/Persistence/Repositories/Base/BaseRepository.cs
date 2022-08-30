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

// TODO: Add As No tracking
public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
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

    public async Task<TEntity> ByIdAsync<TE>(TE id)
    { 
        var entity = await RepositoryContext.Set<TEntity>().FindAsync(id);
        
        if (entity is null)
        {
            throw new NotFoundException(_localizer[ResxKey.NotFoundText, typeof(TE).Name]);
        }

        return entity;
    }
    
    public async Task<TD?> ByIdAsync<TE, TD>(TE id) where TD : class 
        =>  (await RepositoryContext.Set<TEntity>().FindAsync(id))?.Adapt<TD>();
    
    public IQueryable<TEntity> GetAll() => RepositoryContext.Set<TEntity>().AsNoTracking();
    
    public IQueryable<TD> GetAll<TE, TD>(TE id) where TD : class 
        => RepositoryContext.Set<TEntity>().AsNoTracking().ProjectToType<TD>();

    /// <summary>
    /// Gets the first entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <remarks>This method default no-tracking query.</remarks>
    public async Task<TDest> FirstToTypeAsync<TDest>(Expression<Func<TEntity, bool>> predicate = null,
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
    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate = null,
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
    public virtual async Task<TEntity> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = false, bool ignoreQueryFilters = false)
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
            return await orderBy(query).ProjectToType<TEntity>().FirstOrDefaultAsync();
        }
        else
        {
            return await query.ProjectToType<TEntity>().FirstOrDefaultAsync();
        }
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
                                              bool disableTracking = false, bool ignoreQueryFilters = false)
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
            return await orderBy(query).ProjectToType<TDest>().FirstOrDefaultAsync();
        }
        else
        {
            return await query.ProjectToType<TDest>().FirstOrDefaultAsync();
        }
    }
        
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
    
    public IQueryable<TDest> FindByCondition<TDest>(Expression<Func<TEntity, bool>> predicate, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = false, bool ignoreQueryFilters = false)
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
            return orderBy(query).ProjectToType<TDest>();
        }
        else
        {
            return query.ProjectToType<TDest>();
        }
    }

    public async Task<TEntity> CreateAsync(TEntity entity, bool save = false)
    {
        await RepositoryContext.Set<TEntity>().AddAsync(entity);
        if (save)
        {
            await SaveAsync();
        }

        return entity;
    }

    public async Task Update(TEntity entity, bool save = false)
    {
        RepositoryContext.Set<TEntity>().Update(entity);
        if (save)
        {
            await SaveAsync();
        }
    }

    public async Task Delete(TEntity entity, bool save = false)
    {
       RepositoryContext.Set<TEntity>().Remove(entity);
       if (save)
       {
           await SaveAsync();
       }
    }

    public async Task BulkUpdateAsync(IEnumerable<TEntity> entities) => await RepositoryContext.Set<TEntity>().BatchUpdateAsync(entities);

    public async Task BulkInsertAsync(IList<TEntity> entities) => await RepositoryContext.BulkInsertAsync(entities, 
        new BulkConfig()
        {
            SetOutputIdentity = true
        });
    
    public async Task BulkDeleteAsync(IList<TEntity> entities) => await RepositoryContext.BulkDeleteAsync(entities);
    
    public async Task BulkUpsert(IList<TEntity> entities) => await RepositoryContext.BulkInsertOrUpdateAsync(entities,  
        new BulkConfig()
        {
            SetOutputIdentity = true
        });
    
    public async Task BulkUpsertOrDelete(IList<TEntity> entities) => await RepositoryContext.BulkInsertOrUpdateOrDeleteAsync(entities,
        new BulkConfig()
        {
            SetOutputIdentity = true
        });

    public async Task SaveAsync() => await RepositoryContext.SaveChangesAsync();
    
    public async Task BulkSaveAsync() => await RepositoryContext.BulkSaveChangesAsync();

}