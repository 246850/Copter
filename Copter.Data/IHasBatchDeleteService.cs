using System.Collections.Generic;

namespace Copter.Data
{
    /// <summary>
    /// 批量删除 接口
    /// </summary>
    public interface IHasBatchDeleteService<in TKey> where TKey : struct
    {
        /// <summary>
        /// 批量删除， 使用sql执行
        /// </summary>
        /// <param name="primaryKeys">主键Id集合</param>
        /// <returns>影响行数</returns>
        int BatchDelete(IEnumerable<TKey> primaryKeys);
    }

    public interface IInt32BatchDeleteService : IHasBatchDeleteService<int> { }
}
