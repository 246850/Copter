namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 业务逻辑 - 删除接口
    /// </summary>
    public interface IHasDeleteBus<out TResult, in TKey> where TKey:struct 
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        TResult Delete(TKey id);
    }

    public interface  IInt32DeleteBus<out TResult>:IHasDeleteBus<TResult, int> { }
}
