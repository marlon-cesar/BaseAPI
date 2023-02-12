using BaseAPI.Domain.Constants;
using BaseAPI.Domain.Models.Common;
using System.Linq.Expressions;

namespace BaseAPI.Domain.Interfaces.Common
{
    public interface ICrudServiceBase<TEntity> : IServiceBase where TEntity : EntityBase
    {
        Task<TEntity> GetAsync(Guid id);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task SaveAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        bool Any(Guid id);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Query();
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);
        PagedResult<TEntity> Page(IQueryable<TEntity> query, int page, int pageSize = GlobalConstants.DefaultPageSize);
        PagedResult<TEntity> Page(Expression<Func<TEntity, bool>> predicate, int page, int pageSize = GlobalConstants.DefaultPageSize);
    }
}
