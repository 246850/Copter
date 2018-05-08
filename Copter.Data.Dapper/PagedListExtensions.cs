using Copter.Result.List;
using System.Collections.Generic;

namespace Copter.Data.Dapper
{
    internal static class PagedListExtensions
    {
        public static IPagedList<TEntity> ToPagedList<TEntity>(this IEnumerable<TEntity> list, int page, int pageSize, long total) where TEntity : class, new()
        {
            PagedListResult<TEntity> result = new PagedListResult<TEntity>(page, pageSize, total);
            result.AddRange(list);
            return result;
        }
    }
}
