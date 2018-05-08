using System.Collections.Generic;

namespace Copter.Data
{
    /// <summary>
    /// 获取全部列表 接口
    /// </summary>
    public interface IHasAllService<TEntity, TKey> where TEntity : CopterBaseEntity<TKey>, new() where TKey : struct
    {
        /// <summary>
        /// 获取全部列表
        /// </summary>
        /// <returns>TEntity类型列表</returns>
        IList<TEntity> GetAll();
    }

    public interface IInt32AllService<TEntity> : IHasAllService<TEntity, int> where TEntity : CopterBaseEntity<int>, new()
    {

    }
}
