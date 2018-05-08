namespace Copter.Result.Json
{
    /// <summary>
    /// Code 值 结果 类， 无附加数据Body
    /// </summary>
    public class CodeResult : CopterResult, IHasCodeResult, IHasMessageResult
    {
        public CodeResult() : this(ResultConst.SuccessCode, ResultConst.SuccessMessage)
        {

        }

        public CodeResult(long code, string message)
        {
            Code = code;
            Message = message;
        }

        public long Code { get; set; }
        public string Message { get; set; }
    }
}
