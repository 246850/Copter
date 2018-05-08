using System.Collections.Generic;

namespace Copter.Data
{
    /// <summary>
    /// 添加 接口
    /// </summary>
    /// <typeparam name="TEntity">TEntity 对象类型</typeparam>
    /// <typeparam name="TKey">主键Id类型</typeparam>
    public interface IHasAddService<in TEntity, TKey> where TEntity : CopterBaseEntity<TKey>, new() where TKey : struct
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>影响行数|新增Id</returns>
        int Add(TEntity entity);
    }

    /// <summary>
    /// 批量添加 接口
    /// </summary>
    /// <typeparam name="TEntity">TEntity 对象类型</typeparam>
    /// <typeparam name="TKey">主键Id类型</typeparam>
    public interface IHasAddListService<TEntity, TKey> where TEntity : CopterBaseEntity<TKey>, new() where TKey : struct
    {
        /// <summary>
        /// 批量添加 - 接口
        /// </summary>
        /// <param name="entities">TEntity实体对象 列表</param>
        /// <returns>影响行数</returns>
        int AddList(IList<TEntity> entities);
    }

    public interface IInt32AddListService<TEntity>: IHasAddListService<TEntity, int> where TEntity : CopterBaseEntity<int>, new()
    {
    }

    public interface IInt32AddService<in TEntity> : IHasAddService<TEntity, int>
        where TEntity : CopterBaseEntity<int>, new()
    {

    }
}
