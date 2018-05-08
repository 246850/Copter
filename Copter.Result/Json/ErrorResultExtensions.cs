namespace Copter.Result.Json
{
    /// <summary>
    /// ErrorResult 扩展类
    /// </summary>
    public static class ErrorResultExtensions
    {
        /// <summary>
        /// 自定义Code，Message为默认值 - ErrorResult值结果对象
        /// </summary>
        /// <typeparam name="T">类型T</typeparam>
        /// <param name="error">T 错误信息对象</param>
        /// <param name="code">编号</param>
        /// <returns>ErrorResult值结果对象</returns>
        public static ErrorResult<T> ToErrorResult<T>(this T error, long code)
        {
            return ToErrorResult(error, code, ResultConst.ErrorMessage);
        }
        /// <summary>
        /// 自定义Code & Message - ErrorResult值结果对象
        /// </summary>
        /// <typeparam name="T">类型T</typeparam>
        /// <param name="error">T 错误信息对象</param>
        /// <param name="code">编号</param>
        /// <param name="message">提示信息</param>
        /// <returns>ErrorResult值结果对象</returns>
        public static ErrorResult<T> ToErrorResult<T>(this T error, long code, string message)
        {
            return new ErrorResult<T>
            {
                Code = code,
                Message = message,
                Error = error
            };
        }

        /// <summary>
        /// 带登录地址的 授权不通过 UnAuthResult值结果对象 Code=401
        /// </summary>
        /// <param name="loginUrl">登录地址</param>
        /// <returns>UnAuthResult值结果对象</returns>
        public static UnAuthResult ToUnAuthResult(this string loginUrl)
        {
            return new UnAuthResult(loginUrl);
        }

        /// <summary>
        /// 带登录地址的 授权不通过 UnAuthResult值结果对象 自定义 Code,Message
        /// </summary>
        /// <param name="loginUrl">登录地址</param>
        /// <param name="code">编码值</param>
        /// <param name="message">提示信息</param>
        /// <returns>UnAuthResult值结果对象</returns>
        public static UnAuthResult ToUnAuthResult(this string loginUrl, long code, string message)
        {
            return new UnAuthResult(code, message, loginUrl);
        }
    }
}
