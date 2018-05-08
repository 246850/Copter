namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 业务逻辑 - 修改接口
    /// </summary>
    /// <typeparam name="TModel">业务模型 类型</typeparam>
    /// <typeparam name="TResult">返回结果 类型</typeparam>
    /// <typeparam name="TKey">主键 类型</typeparam>
    public interface IHasModifyBus<out TResult, in TModel, TKey> where TModel : CopterBaseModel<TKey>, new() where TKey : struct
    {
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        TResult Modify(TModel model);
    }

    public interface IInt32ModifyBus<out TResult, in TModel> : IHasModifyBus<TResult, TModel, int> where TModel : CopterBaseModel<int>, new()
    {
        
    }
}
