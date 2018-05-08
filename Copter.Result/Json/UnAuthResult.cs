namespace Copter.Result.Json
{
    /// <summary>
    /// 授权不通过结果
    /// </summary>
    public class UnAuthResult : CodeResult
    {
        public UnAuthResult(string loginUrl) : this(401, "授权不通过", loginUrl) { }

        public UnAuthResult(long code, string message, string loginUrl) : base(code, message)
        {
            LoginUrl = LoginUrl;
        }

        /// <summary>
        /// 登录路径地址
        /// </summary>
        public string LoginUrl { get; set; }
    }
}
