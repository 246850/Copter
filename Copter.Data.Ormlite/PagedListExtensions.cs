using Copter.Result.List;
using ServiceStack.OrmLite;
using System.Data;
using System.Linq;

namespace Copter.Data.Ormlite
{
    /// <summary>
    /// ServiceStack.Ormlite 分页扩展类 - IPagedList<TEntity>
    /// </summary>
    internal static class PagedListExtensions
    {
        public static IPagedList<TEntity> ToPagedList<TEntity,TKey>(this SqlExpression<TEntity> expression, IDbConnection connection, int page, int pageSize) 
            where TEntity : CopterBaseEntity<TKey>, new()
             where TKey : struct
        {
            long total = connection.Count(expression);
            PagedListResult<TEntity> result = new PagedListResult<TEntity>(page, pageSize, total);
            result.AddRange(connection.Select(expression.Skip((page - 1) * pageSize).Take(pageSize)));
            return result;
        }
    }
}
