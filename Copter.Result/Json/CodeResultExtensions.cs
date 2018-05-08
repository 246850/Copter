namespace Copter.Result.Json
{
    /// <summary>
    /// CodeResult 扩展类
    /// </summary>
    public static class CodeResultExtensions
    {
        /// <summary>
        /// Message为默认值 - CodeResult值结果对象
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns>CodeResult值结果对象</returns>
        public static CodeResult ToCodeResult(this int code)
        {
            return ToCodeResult(code, code == ResultConst.SuccessCode ? ResultConst.SuccessMessage : ResultConst.FailedMessage);
        }

        /// <summary>
        /// 自定义Code & Message - CodeResult值结果对象
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="message">提示信息</param>
        /// <returns>CodeResult值结果对象</returns>
        public static CodeResult ToCodeResult(this int code, string message)
        {
            return new CodeResult(code, message);
        }

        /// <summary>
        /// Message为默认值 - CodeResult值结果对象
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns>CodeResult值结果对象</returns>
        public static CodeResult ToCodeResult(this long code)
        {
            return ToCodeResult(code, code == ResultConst.SuccessCode ? ResultConst.SuccessMessage : ResultConst.FailedMessage);
        }

        /// <summary>
        /// 自定义Code & Message - CodeResult值结果对象
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="message">提示信息</param>
        /// <returns>CodeResult值结果对象</returns>
        public static CodeResult ToCodeResult(this long code, string message)
        {
            return new CodeResult(code, message);
        }

        /// <summary>
        /// 返回True：默认成功结果，False：默认失败结果 - CodeResult值结果对象
        /// </summary>
        /// <param name="flag">True，False</param>
        /// <returns>CodeResult值结果对象</returns>
        public static CodeResult ToCodeResult(this bool flag)
        {
            return flag ? ResultProvider.CodeSuccess : ResultProvider.CodeFailed;
        }

        /// <summary>
        /// 自定义Message 返回True：默认成功结果，False：默认失败结果 - CodeResult值结果对象
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static CodeResult ToCodeResult(this bool flag, string message)
        {
            return flag ? ToCodeResult(ResultConst.SuccessCode, message) : ToCodeResult(ResultConst.FailedCode, message);
        }

        /// <summary>
        /// Code = body != null ? 0 : -1， Message为默认值 - CodeResult值结果对象 - 泛型
        /// </summary>
        /// <typeparam name="T">类型T</typeparam>
        /// <param name="body">类型T 对象</param>
        /// <returns>CodeResult值结果对象 - 泛型</returns>
        public static CodeResult<T> ToCodeResult<T>(this T body) where T : class, new()
        {
            return ToCodeResult(body, body != null ? ResultConst.SuccessMessage : ResultConst.FailedMessage);
        }

        /// <summary>
        /// Code = body != null ? 0 : -1，自定义Message - Boolean值结果对象 - 泛型
        /// </summary>
        /// <typeparam name="T">类型T</typeparam>
        /// <param name="body">类型T 对象</param>
        /// <param name="message">提示信息</param>
        /// <returns>CodeResult值结果对象 - 泛型</returns>
        public static CodeResult<T> ToCodeResult<T>(this T body, string message) where T : class, new()
        {
            return new CodeResult<T>(body != null ? ResultConst.SuccessCode : ResultConst.FailedCode, message) { Body = body };
        }
    }
}
