namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 应用程序密钥（AppSecret）获取者 接口
    /// </summary>
    public interface ISecretBuilder
    {
        /// <summary>
        /// 应用程序key（AppKey）
        /// </summary>
        string AppKey { get; }
        /// <summary>
        /// 根据 AppKey 获取 AppSecret
        /// </summary>
        /// <returns>AppSecret 字符串</returns>
        string Build();
    }
}
