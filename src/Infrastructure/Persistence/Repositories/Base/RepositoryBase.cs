using System.Linq.Expressions;
using Core.Common.Contracts;
using EFCore.BulkExtensions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Base;

public class RepositoryBase<T>: IRepositoryBase<T> where T : class
{
    protected AppDbContext RepositoryContext { get; set; }

    public RepositoryBase(AppDbContext repositoryContext) 
    {
        RepositoryContext = repositoryContext; 
    }
    
    public async Task<T?> ByIdAsync<TE>(TE id) => await RepositoryContext.Set<T>().FindAsync(id);
    
    public async Task<TD?> ByIdToTypeAsync<TE, TD>(TE id) where TD : class 
        =>  (await RepositoryContext.Set<T>().FindAsync(id))?.Adapt<TD>();
    
    public Task<IQueryable<T>> FindAll() => Task.FromResult(RepositoryContext.Set<T>().AsNoTracking());
    
    public Task<IQueryable<TD>> FindAllToType<TE, TD>(TE id) where TD : class 
        => Task.FromResult(RepositoryContext.Set<T>().AsNoTracking().ProjectToType<TD>());
    
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression) => 
        await RepositoryContext.Set<T>().FirstOrDefaultAsync(expression);
    
    public Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression) => 
        Task.FromResult(RepositoryContext.Set<T>().Where(expression).AsNoTracking());
    
    public Task<IQueryable<TE>> FindByConditionToType<TE> (Expression<Func<T, bool>> expression) => 
        Task.FromResult(RepositoryContext.Set<T>().Where(expression).AsNoTracking().ProjectToType<TE>());
    
    public async Task CreateAsync(T entity) => await RepositoryContext.Set<T>().AddAsync(entity);
    
    public Task Update(T entity) => Task.FromResult(RepositoryContext.Set<T>().Update(entity));
    
    public Task Delete(T entity) => Task.FromResult(RepositoryContext.Set<T>().Remove(entity));
    
    public async Task BulkUpdateAsync(IEnumerable<T> entities) => await RepositoryContext.Set<T>().BatchUpdateAsync(entities);

    public async Task BulkInsertAsync(IList<T> entities) => await RepositoryContext.BulkInsertAsync(entities, 
        new BulkConfig()
        {
            SetOutputIdentity = true
        });
    
    public async Task BulkDeleteAsync(IList<T> entities) => await RepositoryContext.BulkDeleteAsync(entities);

    public async Task SaveAsync(T entity) => await RepositoryContext.SaveChangesAsync();
}