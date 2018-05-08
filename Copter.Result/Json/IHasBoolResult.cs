namespace Copter.Result.Json
{
    /// <summary>
    /// 带 Boolean 值 结果 接口
    /// </summary>
    public interface IHasBoolResult
    {
        /// <summary>
        /// 状态：true or false
        /// </summary>
        bool Status { get; set; }
    }
}
