using BaseAPI.Domain.Constants;
using BaseAPI.Domain.Models.Common;
using System.Linq.Expressions;

namespace BaseAPI.Domain.Interfaces.Common
{
    public interface ICrudRepositoryBase<TEntity> : IRepositoryBase where TEntity : EntityBase
    {
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task SaveAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        IQueryable<TEntity> Query();
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);
        Task<PagedResult<T>> Page<T>(IQueryable<T> query, int page = 1, int pageSize = GlobalConstants.DefaultPageSize);
        Task<PagedResult<TResult>> Page<TSource, TResult>(IQueryable<TSource> query, Expression<Func<TSource, TResult>> selector, int page = 1, int pageSize = GlobalConstants.DefaultPageSize);

    }
}
