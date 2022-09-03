using System.Collections.Generic;

namespace BaseAPI.Domain.Models.Common
{
    public class PagedResult<TEntity>
    {
        public PagedResult(int currentPage, int totalPages, List<TEntity> items)
        {
            this.CurrentPage = currentPage;
            this.TotalPages = totalPages;
            this.Items = items;
        }

        public virtual int CurrentPage { get; set; }
        public virtual int TotalPages { get; set; }
        public virtual List<TEntity> Items { get; set; }
    }
}
