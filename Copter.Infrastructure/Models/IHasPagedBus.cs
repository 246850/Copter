
namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 业务逻辑 分页查询接口
    /// </summary>
    /// <typeparam name="TModel">业务模型 类型</typeparam>
    /// <typeparam name="TKey">主键 类型</typeparam>
    /// <typeparam name="TResult">结果 类型</typeparam>
    public interface IHasPagedBus<out TResult, in TModel, TKey> where TModel : CopterBaseModel<TKey>, new() where TKey : struct
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query">查询对象</param>
        /// <param name="pager">分页数据</param>
        /// <returns>结果 对象</returns>
        TResult GetPagedList(TModel query, CopterPager pager);
    }

    public interface IInt32PagedBus<out TResult, in TModel>: IHasPagedBus<TResult, TModel, int> where TModel : CopterBaseModel<int>, new() { }
}
