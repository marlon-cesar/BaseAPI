using BaseAPI.Domain.Constants;
using BaseAPI.Domain.Interfaces.Common;
using BaseAPI.Domain.Models.Common;
using System.Linq.Expressions;

namespace BaseAPI.Domain.Services.Common
{
    public abstract class CrudServiceBase<TEntity> : ServiceBase, ICrudServiceBase<TEntity> where TEntity : EntityBase
    {
        protected readonly ICrudRepositoryBase<TEntity> _repository;

        public CrudServiceBase(ICrudRepositoryBase<TEntity> repository) => this._repository = repository;

        public virtual async Task<TEntity> GetAsync(Guid id) => await _repository.GetAsync(id);

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await _repository.FirstOrDefaultAsync(predicate);

        public virtual async Task SaveAsync(TEntity entity) => await _repository.SaveAsync(entity);

        public virtual async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate) => Query().Any(predicate);

        public virtual bool Any(Guid id) => Query().Any(e => e.Id.Equals(id));

        public virtual IQueryable<TEntity> Query() => _repository.Query();

        public virtual IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate) => _repository.Query(predicate);

        public virtual PagedResult<TEntity> Page(Expression<Func<TEntity, bool>> predicate, int page, int pageSize = GlobalConstants.DefaultPageSize) => this.Page(this.Query(predicate), page, pageSize);

        public virtual PagedResult<TEntity> Page(IQueryable<TEntity> query, int page, int pageSize = GlobalConstants.DefaultPageSize) =>
            new PagedResult<TEntity>(
                page,
                (int)Math.Ceiling(query.Count() / (decimal)pageSize),
                query.Skip((page - 1) * pageSize).Take(pageSize).ToList()
            );
    }
}
