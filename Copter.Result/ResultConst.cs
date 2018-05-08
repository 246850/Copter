namespace Copter.Result
{
    /// <summary>
    /// 结果数据 相关常量类
    /// </summary>
    public sealed class ResultConst
    {

        /// <summary>
        /// 默认失败编号
        /// </summary>
        public static readonly int FailedCode = -1;
        /// <summary>
        /// 默认成功编号
        /// </summary>
        public static readonly int SuccessCode = 0;
        /// <summary>
        /// 默认成功提示字符串
        /// </summary>
        public static readonly string SuccessMessage = "成功 - Success";
        /// <summary>
        /// 默认失败提示字符串
        /// </summary>
        public static readonly string FailedMessage = "失败 - Failed";
        /// <summary>
        /// 默认错误提示字符串
        /// </summary>
        public static readonly string ErrorMessage = "错误 - Error";

        /// <summary>
        /// 模型验证失败 错误编号
        /// </summary>
        public static readonly int ModelValidateFailedCode = 4001;
        /// <summary>
        /// 模型参数验证失败提示字符串
        /// </summary>
        public static readonly string ModelValidateFailedMessage = "请求模型参数验证失败";

        /// <summary>
        /// 授权验证失败 编号
        /// </summary>
        public static readonly int UnauthorizedCode = 401;
        /// <summary>
        /// 内部服务器错误 编号
        /// </summary>
        public static readonly int InternalServerErrorCode = 500;
        /// <summary>
        /// 内部服务器错误 提示字符串
        /// </summary>
        public static readonly string InternalServerErrorMessage = "内部服务器错误";
    }
}
