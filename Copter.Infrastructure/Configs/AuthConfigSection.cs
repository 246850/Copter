using System.Configuration;

namespace Copter.Infrastructure.Configs
{
    /// <summary>
    /// web 登录授权配置模块
    /// </summary>
    public class AuthConfigSection : ConfigurationSection
    {
        /// <summary>
        /// 授权配置
        /// </summary>
        [ConfigurationProperty("AuthConfig", IsRequired = false)]
        public AuthConfigElement AuthConfig
        {
            get
            {
                AuthConfigElement temp = (AuthConfigElement)base["AuthConfig"];
                if (temp == null) return new AuthConfigElement();
                return temp;
            }
            set { base["AuthConfig"] = value; }
        }
    }
}
