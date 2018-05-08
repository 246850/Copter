namespace Copter.Result.Json
{
    /// <summary>
    /// Boolean & Code 双重判定 结果 类型， 带附加数据Body - 泛型
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    public class ServiceResult<T> : ServiceResult, IHasBodyResult<T>
    {
        public T Body { get; set; }

    }
}
