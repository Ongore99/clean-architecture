using System.Linq.Expressions;

namespace Domain.Common.Contracts;

public interface IRepositoryBase<T> where T : class
{
    Task<T> ByIdAsync<TE>(TE id);
    
    Task<IQueryable<T>> FindAll();

    Task<IQueryable<TD>> FindAllToType<TE, TD>(TE id) where TD : class;
    
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    
    Task<IQueryable<T>> FindByCondition(Expression<Func<T, bool>> expression);

    Task<IQueryable<TE>> FindByConditionToType<TE>(Expression<Func<T, bool>> expression);
    
    Task<TD?> ByIdToTypeAsync<TE, TD>(TE id) where TD : class ;
    
    Task CreateAsync(T entity);
    
    Task Update(T entity);
    
    Task Delete(T entity);

    Task BulkUpdateAsync(IEnumerable<T> entities);

    Task BulkInsertAsync(IList<T> entities);

    Task BulkDeleteAsync(IList<T> entities);

    Task BulkUpsert(IList<T> entities);
    
    Task BulkUpsertOrDelete(IList<T> entities);
    
    Task SaveAsync(T entity);

    Task BulkSaveAsync(T entity);
}