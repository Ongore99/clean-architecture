using System.Linq.Expressions;
using Domain.Common.Constants;
using Domain.Common.Contracts;
using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
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
    
    public async Task<TD?> ByIdToTypeAsync<TE, TD>(TE id) where TD : class 
        =>  (await RepositoryContext.Set<TEntity>().FindAsync(id))?.Adapt<TD>();
    
    public Task<IQueryable<TEntity>> FindAll() => Task.FromResult(RepositoryContext.Set<TEntity>().AsNoTracking());
    
    public Task<IQueryable<TD>> FindAllToType<TE, TD>(TE id) where TD : class 
        => Task.FromResult(RepositoryContext.Set<TEntity>().AsNoTracking().ProjectToType<TD>());

    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await RepositoryContext.Set<TEntity>().FirstOrDefaultAsync(expression);
        if (entity is null)
        {
            throw new NotFoundException(_localizer[ResxKey.NotFoundText, typeof(TEntity).Name]);
        }
        
        return entity;
    }

    public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await RepositoryContext.Set<TEntity>().FirstOrDefaultAsync(expression);
        
        return entity;
    }

    public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
        bool disableTracking = true, 
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

    public Task<IQueryable<TE>> FindByConditionToType<TE> (Expression<Func<TEntity, bool>> expression) => 
        Task.FromResult(RepositoryContext.Set<TEntity>().Where(expression).AsNoTracking().ProjectToType<TE>());

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