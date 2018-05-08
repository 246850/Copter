namespace Copter.Data
{
    /// <summary>
    /// 查找单个 操作
    /// </summary>
    public interface IHasGetService<out TEntity, in TKey> where TEntity : CopterBaseEntity<TKey>, new() where TKey : struct
    {
        /// <summary>
        /// 查询 - 单个
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns>TEntity 对象</returns>
        TEntity Get(TKey id);
    }

    public interface IInt32GetService<out TEntity>:IHasGetService<TEntity,int> where TEntity : CopterBaseEntity<int>, new() { }
}
