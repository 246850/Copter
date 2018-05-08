namespace Copter.Result.Json
{
    /// <summary>
    /// ServiceResult 扩展类
    /// </summary>
    public static class ServiceResultExtensions
    {
        #region bool类型 转 ServiceResult

        /// <summary>
        /// Code = 0，Message为默认值
        /// </summary>
        public static ServiceResult ToServiceResult(this bool status)
        {
            return ToServiceResult(status, ResultConst.SuccessMessage);
        }
        /// <summary>
        /// Code = 0
        /// </summary>
        public static ServiceResult ToServiceResult(this bool status, string message)
        {
            return ToServiceResult(status, ResultConst.SuccessCode, message);
        }
        /// <summary>
        /// 自定义
        /// </summary>
        public static ServiceResult ToServiceResult(this bool status, int code, string message)
        {
           return new ServiceResult(status, code, message);
        }

        #endregion

        #region Int32/Int64类型 转 ServiceResult
        /// <summary>
        /// Status = True，Message为默认值
        /// </summary>
        public static ServiceResult ToServiceResult(this long code)
        {
            return ToServiceResult(code, ResultConst.SuccessMessage);
        }
        /// <summary>
        /// Status = True
        /// </summary>
        public static ServiceResult ToServiceResult(this long code, string message)
        {
            return ToServiceResult(code, true, message);
        }
        /// <summary>
        /// 自定义
        /// </summary>
        public static ServiceResult ToServiceResult(this long code, bool status, string message)
        {
            return new ServiceResult(status, code, message);
        }
        #endregion

        #region 泛型结果 转 ServiceResult<T>
        /// <summary>
        /// Status = True，Code = 0，Message为默认值
        /// </summary>
        public static ServiceResult ToServiceResult<T>(this T body) where T : class, new()
        {
            return ToServiceResult(body, true, ResultConst.SuccessCode, ResultConst.SuccessMessage);
        }
        /// <summary>
        /// 自定义
        /// </summary>
        public static ServiceResult ToServiceResult<T>(this T body, bool status, int code, string message)
        {
            return new ServiceResult<T>
            {
                Status = status,
                Code = code,
                Message = message,
                Body = body
            };
        }
        #endregion
    }
}
