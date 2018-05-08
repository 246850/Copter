namespace Copter.Result.Json
{
    /// <summary>
    /// 错误 结果 泛型 - 带附件错误数据
    /// </summary>
    /// <typeparam name="T">泛型类型，代表错误信息对象</typeparam>
    public class ErrorResult<T> : CodeResult
    {
        public ErrorResult()
        {
            
        }
        public ErrorResult(long code, string message, T error)
        {
            Code = code;
            Message = message;
            Error = error;
        }
        /// <summary>
        /// 错误信息对象数据载体
        /// </summary>
        public T Error { get; set; }
    }
}
