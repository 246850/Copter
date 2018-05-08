namespace Copter.Data
{
    /// <summary>
    /// 删除 接口
    /// </summary>
    public interface IHasDeleteService<in TKey> where TKey : struct
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"> 主键Id</param>
        /// <returns>影响行数</returns>
        int Delete(TKey id);
    }

    public interface IInt32DeleteService : IHasDeleteService<int> { }
}
