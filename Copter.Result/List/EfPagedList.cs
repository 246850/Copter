using System.Linq;

namespace Copter.Result.List
{
    public class EfPagedList<T> where T : class, new()
    {
        private readonly PagedListResult<T> _pageResultList;

        public EfPagedList(IQueryable<T> queryable, int page, int pageSize)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 10)
                pageSize = 10;
            int total = queryable.Count();
            _pageResultList = new PagedListResult<T>(page, pageSize, total);
            _pageResultList.AddRange(queryable.Skip((page - 1) * pageSize).Take(pageSize));
        }

        public IPagedList<T> ToList()
        {
            return _pageResultList ?? new PagedListResult<T>();
        }
    }
}
