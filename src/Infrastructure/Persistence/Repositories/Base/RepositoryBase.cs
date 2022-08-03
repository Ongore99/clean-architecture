using System.Linq.Expressions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories.Base;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected AppDbContext RepositoryContext { get; set; }
    
    public RepositoryBase(AppDbContext repositoryContext) 
    {
        RepositoryContext = repositoryContext; 
    }
    
    public async Task<T?> FindById<E>(E id) => await RepositoryContext.Set<T>().FindAsync(id);
    
    public IQueryable<T> FindAll() => RepositoryContext.Set<T>().AsNoTracking();
    
    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => 
        RepositoryContext.Set<T>().Where(expression).AsNoTracking();
    
    public async Task Create(T entity) => await RepositoryContext.Set<T>().AddAsync(entity);
    
    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
    
    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    
    public async Task BulkUpdate(IEnumerable<T> entities) => await RepositoryContext.Set<T>().BatchUpdateAsync(entities);
    
    public async Task BulkDelete(IEnumerable<T> entities) => await RepositoryContext.Set<T>().BatchDeleteAsync(entities);
}