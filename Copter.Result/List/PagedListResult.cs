using System.Collections.Generic;

namespace Copter.Result.List
{
    public class PagedListResult<T> : List<T>, IPagedList<T> where T : class, new()
    {
        public PagerItem Pager { get; }
        public PagedListResult() : this(1, 15, 0) { }

        public PagedListResult(int page, int pageSize, long total)
        {
            Pager = new PagerItem(page, pageSize, total);
        }
    }
}
