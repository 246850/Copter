namespace Copter.Result.Json
{
    /// <summary>
    /// 带 提示信息Message 结果 接口 
    /// </summary>
    public interface IHasMessageResult
    {
        /// <summary>
        /// 提示信息
        /// </summary>
        string Message { get; set; }
    }
}
