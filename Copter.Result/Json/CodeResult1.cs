namespace Copter.Result.Json
{
    /// <summary>
    /// Code 值 结果 类， 无附加数据Body
    /// </summary>
    /// <typeparam name="T">类型T</typeparam>
    public class CodeResult<T> : CodeResult, IHasBodyResult<T>
    {
        public CodeResult() : this(ResultConst.SuccessCode, ResultConst.SuccessMessage)
        {
        }
        public CodeResult(long code, string message) : base(code, message)
        {
        }
        public virtual T Body { get; set; }
    }
}
