using Copter.Infrastructure.Configs;

namespace Copter.Infrastructure.Models
{
    /// <summary>
    /// 默认获取配置文件的 Secret
    /// </summary>
    public class DefaultSecretBuilder : ISecretBuilder
    {
        public DefaultSecretBuilder() : this(string.Empty)
        {

        }
        public DefaultSecretBuilder(string appKey)
        {
            AppKey = appKey;
        }

        public string AppKey { get; }

        public string Build()
        {
            return AuthConfigProvider.AuthConfig.AppSecret;
        }
    }
}
