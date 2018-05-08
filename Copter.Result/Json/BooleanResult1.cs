namespace Copter.Result.Json
{
    /// <summary>
    /// 泛型Boolean 值 结果 类型,  带附加数据Body
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BooleanResult<T>: BooleanResult, IHasBodyResult<T>
    {
        /// <summary>
        /// 默认为true
        /// </summary>
        public BooleanResult() : this(true, ResultConst.SuccessMessage)
        { 
        }
        public BooleanResult(bool status, string message): base(status, message)
        {
        }
        public virtual T Body { get; set; }
    }
}
