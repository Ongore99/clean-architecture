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
public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    private readonly IStringLocalizer<SharedResource> _localizer;
    
    private AppDbContext RepositoryContext { get; set; }
    
    public bool WithTracking { get; set; } = false;

    public BaseRepository(AppDbContext repositoryContext, IStringLocalizer<SharedResource> _localizer)
    {
        this._localizer = _localizer;
        RepositoryContext = repositoryContext;
    }

    public async Task<T> ByIdAsync<TE>(TE id)
    { 
        var entity = await RepositoryContext.Set<T>().FindAsync(id);
        
        if (entity is null)
        {
            throw new NotFoundException(_localizer[ResxKey.NotFoundText, typeof(TE).Name]);
        }

        return entity;
    }
    
    public async Task<TD?> ByIdToTypeAsync<TE, TD>(TE id) where TD : class 
        =>  (await RepositoryContext.Set<T>().FindAsync(id))?.Adapt<TD>();
    
    public Task<IQueryable<T>> FindAll() => Task.FromResult(RepositoryContext.Set<T>().AsNoTracking());
    
    public Task<IQueryable<TD>> FindAllToType<TE, TD>(TE id) where TD : class 
        => Task.FromResult(RepositoryContext.Set<T>().AsNoTracking().ProjectToType<TD>());

    public async Task<T> FirstAsync(Expression<Func<T, bool>> expression)
    {
        var entity = await RepositoryContext.Set<T>().FirstOrDefaultAsync(expression);
        if (entity is null)
        {
            throw new NotFoundException(_localizer[ResxKey.NotFoundText, typeof(T).Name]);
        }
        
        return entity;
    }
    
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        var entity = await RepositoryContext.Set<T>().FirstOrDefaultAsync(expression);
        
        return entity;
    }
    
    public Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression) => 
        Task.FromResult(RepositoryContext.Set<T>().Where(expression).AsNoTracking());
    
    public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
    {
        IIncludableQueryable<T, object> query = null;

        if(includes.Length > 0)
        {
            query = RepositoryContext.Set<T>().Include(includes[0]);
        }
        for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
        {
            query = query!.Include(includes[queryIndex]);
        }

        return query == null ? RepositoryContext.Set<T>() : query;
    }
    
    public Task<IQueryable<TE>> FindByConditionToType<TE> (Expression<Func<T, bool>> expression) => 
        Task.FromResult(RepositoryContext.Set<T>().Where(expression).AsNoTracking().ProjectToType<TE>());

    public async Task<T> CreateAsync(T entity, bool save = false)
    {
        await RepositoryContext.Set<T>().AddAsync(entity);
        if (save)
        {
            await SaveAsync();
        }

        return entity;
    }

    public async Task Update(T entity, bool save = false)
    {
        RepositoryContext.Set<T>().Update(entity);
        if (save)
        {
            await SaveAsync();
        }
    }

    public async Task Delete(T entity, bool save = false)
    {
       RepositoryContext.Set<T>().Remove(entity);
       if (save)
       {
           await SaveAsync();
       }
    }

    public async Task BulkUpdateAsync(IEnumerable<T> entities) => await RepositoryContext.Set<T>().BatchUpdateAsync(entities);

    public async Task BulkInsertAsync(IList<T> entities) => await RepositoryContext.BulkInsertAsync(entities, 
        new BulkConfig()
        {
            SetOutputIdentity = true
        });
    
    public async Task BulkDeleteAsync(IList<T> entities) => await RepositoryContext.BulkDeleteAsync(entities);
    
    public async Task BulkUpsert(IList<T> entities) => await RepositoryContext.BulkInsertOrUpdateAsync(entities,  
        new BulkConfig()
        {
            SetOutputIdentity = true
        });
    
    public async Task BulkUpsertOrDelete(IList<T> entities) => await RepositoryContext.BulkInsertOrUpdateOrDeleteAsync(entities,
        new BulkConfig()
        {
            SetOutputIdentity = true
        });

    public async Task SaveAsync() => await RepositoryContext.SaveChangesAsync();
    
    public async Task BulkSaveAsync() => await RepositoryContext.BulkSaveChangesAsync();

}