using System;

namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 授权参数 实体类
    /// </summary>
    [Serializable]
    public class AuthParameterModel
    {
        /// <summary>
        /// 应用程序 唯一标识Key
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public long Timestamp { get; set; }
        /// <summary>
        /// 随机字符串[a-zA-Z0-9]
        /// </summary>
        public string Nonce { get; set; }
        /// <summary>
        /// 加密签名
        /// </summary>
        public string Signature { get; set; }

    }
}
