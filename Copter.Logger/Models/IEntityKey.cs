namespace Copter.Logger.Models
{
    /// <summary>
    /// 日志主键类型 接口
    /// </summary>
    public interface IEntityKey<TKey> where TKey : struct
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        TKey Id { get; set; }
    }
}
