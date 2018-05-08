using System;
using System.Configuration;

namespace Copter.Infrastructure.Configs
{
    /// <summary>
    /// web登录授权配置获取 提供类。
    /// </summary>
    public sealed class AuthConfigProvider
    {
        /// <summary>
        /// 登录授权配置
        /// </summary>
        public static readonly AuthConfigElement AuthConfig;
        static AuthConfigProvider()
        {
            AuthConfigSection section = (AuthConfigSection)ConfigurationManager.GetSection("CopterAuth");
            if (section == null) throw new Exception("未找到CopterAuth配置模块");
            AuthConfig = section.AuthConfig;
        }
    }
}
