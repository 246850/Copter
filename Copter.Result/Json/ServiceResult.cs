namespace Copter.Result.Json
{
    /// <summary>
    /// Boolean & Code 双重判定 结果 类型， 无附加数据Body
    /// </summary>
    public class ServiceResult : CopterResult, IHasBoolResult, IHasCodeResult, IHasMessageResult
    {

        public ServiceResult() : this(true, ResultConst.SuccessCode, ResultConst.SuccessMessage)
        {
        }
        public ServiceResult(bool status, long code, string message)
        {
            Status = status;
            Code = code;
            Message = message;
        }
        public bool Status { get; set; }
        public long Code { get; set; }
        public string Message { get; set; }
    }
}
