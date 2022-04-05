using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinalProject.Models
{
    public class PaginatedList<T>:List<T>
    {
        public PaginatedList(List<T> items, int pagesize, int pageindex, int count)
        {
            this.PageIndex = pageindex;
            this.TotalPages = (int)Math.Ceiling(count / (double)pagesize);
            this.AddRange(items);
        }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext => PageIndex < TotalPages;
        public bool HasPrevious => PageIndex > 1;

        public static PaginatedList<T> Create(IQueryable<T> query, int pagesize, int pageindex)
        {
            var items = query.Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();
            return new PaginatedList<T>(items, pagesize, pageindex, query.Count());
        }
    }
}
