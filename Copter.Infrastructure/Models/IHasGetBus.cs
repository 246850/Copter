namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 业务逻辑 - 查询单个接口
    /// </summary>
    /// <typeparam name="TResult">返回结果类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IHasGetBus<out TResult, in TKey>
        where TResult : new()
        where TKey : struct
    {
        /// <summary>
        /// 查询 - 单个
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        TResult Get(TKey id);
    }

    public interface IInt32GetBus<out TResult> : IHasGetBus<TResult, int> where TResult : new(){ }
}
