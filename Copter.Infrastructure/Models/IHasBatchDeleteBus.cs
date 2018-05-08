using System.Collections.Generic;

namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 业务逻辑 - 批量删除接口
    /// </summary>
    public interface IHasBatchDeleteBus<out TResult, in TKey> where TKey:struct 
    {
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="primaryKeys">主键Id 集合</param>
        /// <returns></returns>
        TResult BatchDelete(IEnumerable<TKey> primaryKeys);
    }

    public interface IInt32BatchDeleteBus<out TResult> : IHasBatchDeleteBus<TResult, int>
    {
        
    }
}
