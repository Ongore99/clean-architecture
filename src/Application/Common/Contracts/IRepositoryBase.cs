using System.Linq.Expressions;

namespace Core.Common.Contracts;

public interface IRepositoryBase<T>
{
    Task<T?> FindByIdAsync<TE>(TE id);
    IQueryable<T> FindAll();
    
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
    
    Task CreateAsync(T entity);
    
    void Update(T entity);
    
    void Delete(T entity);

    Task BulkUpdateAsync(IEnumerable<T> entities);

    Task BulkInsertAsync(IList<T> entities);

    Task BulkDeleteAsync(IList<T> entities);
    
    Task SaveAsync(T entity);
}