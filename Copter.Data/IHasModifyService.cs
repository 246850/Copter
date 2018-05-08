namespace Copter.Data
{
    /// <summary>
    /// 修改 接口
    /// </summary>
    /// <typeparam name="TEntity">TEntity 对象类型</typeparam>
    /// <typeparam name="TKey">主键Id类型</typeparam>
    public interface IHasModifyService<in TEntity, TKey> where TEntity : CopterBaseEntity<TKey>, new()
        where TKey : struct
    {
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">TEntity 对象</param>
        /// <returns>影响行数</returns>
        int Modify(TEntity entity);
    }

    public interface IInt32ModifyService<in TEntity> : IHasModifyService<TEntity, int>
        where TEntity : CopterBaseEntity<int>, new()
    {
    }
}
