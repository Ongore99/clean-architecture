using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Domain.Common.Contracts;

public interface  IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> ByIdAsync<TE>(TE id);
    Task<TD?> ByIdAsync<TE, TD>(TE id) where TD : class;
    Task<TEntity> ByIdsAsync<TE>(params object[] keyValues);
    Task<TDest?> ByIdsToAsync<TDest>(params object[] keyValues) where TDest : class;

    Task<IQueryable<TEntity>> GetAllAsync(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true, bool ignoreQueryFilters = false);

    Task<IQueryable<TD>> GetAll<TE, TD>(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true,
        bool ignoreQueryFilters = false) where TD : class;

    /// <summary>
    /// Gets the first entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <remarks>This method default no-tracking query.</remarks>
    Task<TDest> FirstToAsync<TDest>(Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = false, bool ignoreQueryFilters = false);

    /// <summary>
    /// Gets the first entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <remarks>This method default no-tracking query.</remarks>
    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = false, bool ignoreQueryFilters = false);

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <remarks>This method default no-tracking query.</remarks>
    Task<TDest> GetFirstOrDefaultAsync<TDest>(Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = false, 
        bool ignoreQueryFilters = false);

    /// <summary>
    /// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>false</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>false</c>.</param>
    /// <param name="ignoreQueryFilters">Ignore query filters</param>
    /// <remarks>This method default no-tracking query.</remarks>
    Task<TEntity> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = false, 
        bool ignoreQueryFilters = false);

    IQueryable<TDest> FindByCondition<TDest>(Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool disableTracking = false, bool ignoreQueryFilters = false);

    IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
        bool disableTracking = false, 
        bool ignoreQueryFilters = false);

    Task<TEntity> CreateAsync(TEntity entity, bool save = false);
    Task Update(TEntity entity, bool save = false);
    Task Delete(TEntity entity, bool save = false);
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
    Task<T> MaxAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null);
    Task<T> MinAsync<T>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, T>> selector = null);
    Task<decimal> AverageAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> selector = null);
    Task<decimal> SumAsync(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, decimal>> selector = null);
    Task BulkUpdateAsync(IEnumerable<TEntity> entities);
    Task BulkInsertAsync(IList<TEntity> entities);
    Task BulkDeleteAsync(IList<TEntity> entities);
    Task BulkUpsert(IList<TEntity> entities);
    Task BulkUpsertOrDelete(IList<TEntity> entities);
    Task SaveAsync();
    Task BulkSaveAsync();
}