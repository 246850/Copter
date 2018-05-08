using System.Collections.Generic;

namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 业务逻辑 - 添加接口
    /// </summary>
    /// <typeparam name="TModel">业务模型 类型</typeparam>
    /// <typeparam name="TResult">返回结果 类型</typeparam>
    /// <typeparam name="TKey">主键 类型</typeparam>
    public interface IHasAddBus<out TResult, in TModel, TKey> 
        where TModel: CopterBaseModel<TKey>, new() 
        where TKey : struct
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">业务模型</param>
        /// <returns></returns>
        TResult Add(TModel model);
    }

    public interface IHasAddListBus<out TResult, TModel, TKey>
        where TModel : CopterBaseModel<TKey>, new()
        where TKey : struct
    {
        /// <summary>
        /// 批量 添加
        /// </summary>
        /// <param name="models">业务模型 集合</param>
        /// <returns></returns>
        TResult AddList(IList<TModel> models);
    }

    public interface IInt32AddBus<out TResult, in TModel>:IHasAddBus<TResult, TModel, int> 
        where TModel : CopterBaseModel<int>, new()
    {
         
    }

    public interface IInt32AddListBus<out TResult, TModel> : IHasAddListBus<TResult, TModel, int>
        where TModel : CopterBaseModel<int>, new()
    {

    }
}
