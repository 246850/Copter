namespace Copter.Result.Json
{
    /// <summary>
    /// 带附加数据 Body 结果 接口
    /// </summary>
    /// <typeparam name="T">类型 T</typeparam>
    public interface IHasBodyResult<T>
    {
        /// <summary>
        /// 附加数据载体
        /// </summary>
        T Body { get; set; }
    }
}
