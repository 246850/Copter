namespace Copter.Result.Json
{
    /// <summary>
    /// BooleanResult结果 扩展类
    /// </summary>
    public static class BooleanResultExtensions
    {
        /// <summary>
        /// Message为默认值 - Boolean值结果对象
        /// </summary>
        /// <param name="status">True | False</param>
        /// <returns>BooleanResult结果对象</returns>
        public static BooleanResult ToBoolResult(this bool status)
        {
            return ToBoolResult(status, status ? ResultConst.SuccessMessage : ResultConst.FailedMessage);
        }

        /// <summary>
        /// 自定义 Status & Message - Boolean值结果对象
        /// </summary>
        /// <param name="status">True | False</param>
        /// <param name="message">提示信息</param>
        /// <returns>BooleanResult结果对象</returns>
        public static BooleanResult ToBoolResult(this bool status, string message)
        {
            return new BooleanResult(status, message);
        }

        /// <summary>
        /// Status = number > 0，Message为默认值 - Boolean值结果对象
        /// </summary>
        /// <param name="number">数值</param>
        /// <returns>BooleanResult结果对象</returns>
        public static BooleanResult ToBoolResult(this int number)
        {
            return ToBoolResult((long)number);
        }

        /// <summary>
        /// Status = number > 0，自定义Message - Boolean值结果对象
        /// </summary>
        /// <param name="number">数值</param>
        /// <param name="message">提示信息</param>
        /// <returns>BooleanResult结果对象</returns>
        public static BooleanResult ToBoolResult(this int number, string message)
        {
            return ToBoolResult((long)number, message);
        }

        /// <summary>
        /// Status = number > 0，Message为默认值 - Boolean值结果对象
        /// </summary>
        /// <param name="number">数值</param>
        /// <returns>BooleanResult结果对象</returns>
        public static BooleanResult ToBoolResult(this long number)
        {
            return ToBoolResult(number, number > 0 ? ResultConst.SuccessMessage : ResultConst.FailedMessage);
        }

        /// <summary>
        /// Status = number > 0，自定义Message - Boolean值结果对象
        /// </summary>
        /// <param name="number">数值</param>
        /// <param name="message">提示信息</param>
        /// <returns>BooleanResult结果对象</returns>
        public static BooleanResult ToBoolResult(this long number, string message)
        {
            return new BooleanResult(number > 0, message);
        }

        /// <summary>
        /// Status = body != null， Message为默认值 - Boolean值结果对象 - 泛型
        /// </summary>
        /// <typeparam name="T">类型T</typeparam>
        /// <param name="body">类型T 对象</param>
        /// <returns>BooleanResult结果对象 - 泛型</returns>
        public static BooleanResult<T> ToBoolResult<T>(this T body) where T:class, new()
        {
            return ToBoolResult(body, body != null ? ResultConst.SuccessMessage : ResultConst.FailedMessage);
        }

        /// <summary>
        /// Status = body != null - Boolean值结果对象 - 泛型
        /// </summary>
        /// <typeparam name="T">类型T</typeparam>
        /// <param name="body">类型T 对象</param>
        /// <param name="message">提示信息</param>
        /// <returns>BooleanResult结果对象 - 泛型</returns>
        public static BooleanResult<T> ToBoolResult<T>(this T body, string message) where T : class, new()
        {
            return new BooleanResult<T>(body != null, message) { Body = body };
        }
    }
}
