using System.Collections.Generic;

namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 业务逻辑 - 查询全部
    /// </summary>
    /// <typeparam name="TModel">业务模型 类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IHasAllBus<TModel, TKey> where TModel : CopterBaseModel<TKey>, new() where TKey : struct
    {
        /// <summary>
        /// 查询 - 全部
        /// </summary>
        /// <returns></returns>
        List<TModel> GetAll();
    }

    public interface IInt32AllBus<TModel> : IHasAllBus<TModel, int> where TModel : CopterBaseModel<int>, new() { }
}
