using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Domain.Common.Contracts;

public interface  IBaseRepository<T> where T : class
{
    Task<T> ByIdAsync<TE>(TE id);
    
    Task<TD?> ByIdAsync<TE, TD>(TE id) where TD : class ;
    
    Task<T> CreateAsync(T entity, bool save = false);
    
    Task Update(T entity, bool save = false);
    
    Task Delete(T entity, bool save = false);

    Task BulkUpdateAsync(IEnumerable<T> entities);

    Task BulkInsertAsync(IList<T> entities);

    Task BulkDeleteAsync(IList<T> entities);

    Task BulkUpsert(IList<T> entities);
    
    Task BulkUpsertOrDelete(IList<T> entities);
    
    Task SaveAsync();

    Task BulkSaveAsync();

    IQueryable<T> FindByCondition(Expression<Func<T, bool>> predicate, 
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, 
        bool disableTracking = true, bool ignoreQueryFilters = false);
    
    IQueryable<TDest> FindByCondition<TDest>(Expression<Func<T, bool>> predicate, 
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, 
        bool disableTracking = true, bool ignoreQueryFilters = false);
}