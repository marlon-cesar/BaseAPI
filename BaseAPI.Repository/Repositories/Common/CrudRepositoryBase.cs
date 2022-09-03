namespace BaseAPI.Repository.Repositories.Common
{
    using BaseAPI.Domain.Constants;
    using BaseAPI.Domain.Interfaces.Common;
    using BaseAPI.Domain.Models.Common;
    using BaseAPI.Repository.Contexts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public abstract class CrudRepositoryBase<TEntity> : RepositoryBase, ICrudRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected MainDbContext _context;
        protected DbSet<TEntity> _set;

        public CrudRepositoryBase(MainDbContext context)
        {
            this._context = context;
            this._set = this._context.Set<TEntity>();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await this.GetAsync(id);

            if (entity == null)
                return;

            this._set.Remove(entity);
            await this._context.SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(Guid id) => await this.Query().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await _set.FirstOrDefaultAsync(predicate);

        public IQueryable<TEntity> Query() => _set;

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate) => _set.Where(predicate);

        public virtual async Task<PagedResult<T>> Page<T>(IQueryable<T> query, int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var totalItems = query.Count();

            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var startIndex = ((page - 1) * pageSize);

            if (totalItems > 0)
            {
                var items = await query
                    .Skip(startIndex)
                    .Take(pageSize)
                    .ToListAsync();

                return new PagedResult<T>(page, totalPages, items);
            }

            return new PagedResult<T>(page, totalPages, new List<T>());
        }

        public virtual PagedResult<T> Page<T>(IEnumerable<T> query, int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var totalItems = query.Count();

            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var startIndex = ((page - 1) * pageSize);

            if (totalItems > 0)
            {
                var items = query
                    .Skip(startIndex)
                    .Take(pageSize)
                    .ToList();

                return new PagedResult<T>(page, totalPages, items);
            }

            return new PagedResult<T>(page, totalPages, new List<T>());
        }

        public virtual async Task<PagedResult<TResult>> Page<TSource, TResult>(IQueryable<TSource> query, Expression<Func<TSource, TResult>> selector, int page = 1, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var totalItems = query.Count();

            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var startIndex = ((page - 1) * pageSize);
            var items = await query
                .Skip(startIndex)
                .Take(pageSize)
                .Select(selector)
                .ToListAsync();

            return new PagedResult<TResult>(page, totalPages, items);
        }

        public async Task SaveAsync(TEntity entity)
        {
            var now = DateTime.Now;
            entity.DataAlteracao = now;

            if (entity.Id == null || entity.Id == Guid.Empty)
            {
                entity.DataCriacao = now;
                await this._set.AddAsync(entity);
            }
            else
                this._context.Entry(entity).State = EntityState.Modified;

            await this._context.SaveChangesAsync();
        }
    }
}
