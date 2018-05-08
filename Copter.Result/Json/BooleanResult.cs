namespace Copter.Result.Json
{
    /// <summary>
    /// Boolean 值 结果 类型,  无附加数据Body
    /// </summary>
    public class BooleanResult: CopterResult, IHasBoolResult,IHasMessageResult
    {
        /// <summary>
        /// 默认为true
        /// </summary>
        public BooleanResult():this(true, ResultConst.SuccessMessage)
        {
            
        }

        public BooleanResult(bool status, string message)
        {
            Status = status;
            Message = message;
        }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
