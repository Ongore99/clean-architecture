using System.Linq.Expressions;
using Core.Common.Contracts;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Base;

public abstract class RepositoryBase<T>: IRepositoryBase<T> where T : class
{
    protected AppDbContext RepositoryContext { get; set; }

    protected RepositoryBase(AppDbContext repositoryContext) 
    {
        RepositoryContext = repositoryContext; 
    }
    
    public async Task<T?> FindByIdAsync<TE>(TE id) => await RepositoryContext.Set<T>().FindAsync(id);
    
    public IQueryable<T> FindAll() => RepositoryContext.Set<T>().AsNoTracking();
    
    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression) => await RepositoryContext.Set<T>().FirstOrDefaultAsync(expression);
    
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => 
        RepositoryContext.Set<T>().Where(expression).AsNoTracking();
    
    public async Task CreateAsync(T entity) => await RepositoryContext.Set<T>().AddAsync(entity);
    
    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
    
    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    
    public async Task BulkUpdateAsync(IEnumerable<T> entities) => await RepositoryContext.Set<T>().BatchUpdateAsync(entities);

    public async Task BulkInsertAsync(IList<T> entities) => await RepositoryContext.BulkInsertAsync(entities, 
        new BulkConfig()
        {
            SetOutputIdentity = true
        });
    
    public async Task BulkDeleteAsync(IList<T> entities) => await RepositoryContext.BulkDeleteAsync(entities);

    public async Task SaveAsync(T entity) => await RepositoryContext.SaveChangesAsync();
}