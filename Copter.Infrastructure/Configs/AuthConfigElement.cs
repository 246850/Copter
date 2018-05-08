using System.Configuration;
using System;

namespace Copter.Infrastructure.Configs
{
    /// <summary>
    /// web登录授权配置 类型
    /// </summary>
    public sealed class AuthConfigElement : ConfigurationElement
    {
        /// <summary>
        /// cookie名称 - 默认：.ASPXAUTH
        /// </summary>
        [ConfigurationProperty("CookieName", IsRequired = false, DefaultValue = ".ASPXAUTH")]
        public string CookieName
        {
            get
            {
                string value = (string)base["CookieName"];
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("CookieName", "CookieName配置为空或null");
                return value;
            }
            set
            {
                base["CookieName"] = value;
            }
        }
        /// <summary>
        /// 登录地址 - 默认：/Account/Login
        /// </summary>
        [ConfigurationProperty("LoginUrl", IsRequired = false, DefaultValue = "/Account/Login")]
        public string LoginUrl
        {
            get
            {
                string value = (string)base["LoginUrl"];
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("LoginUrl", "LoginUrl配置为空或null");
                return value;
            }
            set
            {
                base["LoginUrl"] = value;
            }
        }
        /// <summary>
        /// 登录成功跳转的主页 - 默认：/Home/Index
        /// </summary>
        [ConfigurationProperty("DefaultUrl", IsRequired = false, DefaultValue = "/Home/Index")]
        public string DefaultUrl
        {
            get
            {
                string value = (string)base["DefaultUrl"];
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("DefaultUrl", "DefaultUrl配置为空或null");
                return value;
            }
            set
            {
                base["DefaultUrl"] = value;
            }
        }
        /// <summary>
        /// cookie失效时间 单位：秒 - 默认：3600
        /// </summary>
        [ConfigurationProperty("Expires", IsRequired = false, DefaultValue = 3600)]
        public int Expires
        {
            get
            {
                int value = (int)base["Expires"];
                if (value <= 0) throw new Exception("Expires配置不能为0或负数");
                return value;
            }
            set
            {
                base["Expires"] = value;
            }
        }

        /// <summary>
        /// Jwt使用算法类型 - 仅限jwt方式加密时用 - 默认：HS256
        /// </summary>
        [ConfigurationProperty("JwtAlgorithmType", IsRequired = false, DefaultValue = "HS256")]
        public string JwtAlgorithmType
        {
            get
            {
                string value = (string)base["JwtAlgorithmType"];
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("JwtAlgorithmType", "JwtAlgorithmType配置为空或null");
                return value;
            }
            set
            {
                base["JwtAlgorithmType"] = value;
            }
        }

        /// <summary>
        /// 应用Key - 默认：2d7d0204c3f044328902b8bf80207bbd
        /// </summary>
        [ConfigurationProperty("AppKey", IsRequired = false, DefaultValue = "cf7dbd6f43624b5b8aab3744ced09e40")]
        public string AppKey
        {
            get
            {
                string value = (string)base["AppKey"];
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("AppKey", "AppKey配置为空或null");
                return value;
            }
            set
            {
                base["AppKey"] = value;
            }
        }

        /// <summary>
        /// 应用密钥 - 默认：2d7d0204c3f044328902b8bf80207bbd
        /// </summary>
        [ConfigurationProperty("AppSecret", IsRequired = false, DefaultValue = "2d7d0204c3f044328902b8bf80207bbd")]
        public string AppSecret
        {
            get
            {
                string value = (string)base["AppSecret"];
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException("AppSecret", "AppSecret配置为空或null");
                return value;
            }
            set
            {
                base["AppSecret"] = value;
            }
        }
    }
}
