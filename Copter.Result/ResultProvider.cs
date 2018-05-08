using Copter.Result.Json;

namespace Copter.Result
{
    /// <summary>
    /// 常用返回结果 - 提供类
    /// </summary>
    public sealed class ResultProvider
    {
        /// <summary>
        /// 默认成功 - BooleanResult结果
        /// </summary>
        public static readonly BooleanResult BoolSuccess = new BooleanResult(true, ResultConst.SuccessMessage);
        /// <summary>
        /// 默认失败 - BooleanResult结果
        /// </summary>
        public static readonly BooleanResult BoolFailed = new BooleanResult(false, ResultConst.FailedMessage);

        /// <summary>
        /// 默认成功 - CodeResult结果
        /// </summary>
        public static readonly CodeResult CodeSuccess = new CodeResult(ResultConst.SuccessCode, ResultConst.SuccessMessage);
        /// <summary>
        /// 默认失败 - CodeResult结果
        /// </summary>
        public static readonly CodeResult CodeFailed = new CodeResult(ResultConst.FailedCode, ResultConst.FailedMessage);

        /// <summary>
        /// 默认成功 - ServiceResult结果
        /// </summary>
        public static readonly ServiceResult ServiceSuccess = new ServiceResult(true, ResultConst.SuccessCode, ResultConst.SuccessMessage);

        /// <summary>
        /// 默认失败 - ServiceResult结果
        /// </summary>
        public static readonly ServiceResult ServiceFailed = new ServiceResult(false, ResultConst.FailedCode, ResultConst.SuccessMessage);
    }
}
