using Copter.Infrastructure.Configs;
using Copter.Infrastructure.ValueObject;
using System;

namespace Copter.Web.Mvc.Principal
{
    /// <summary>
    /// 授权用户信息 抽象基类
    /// </summary>
    public abstract class AuthUserBase<TKey>
    {
        /// <summary>
        /// 设置了 exp 为默认值
        /// </summary>
        protected AuthUserBase()
        {
            exp = DateTime.Now.AddSeconds(AuthConfigProvider.AuthConfig.Expires).ToTimestamp();
        }
        protected AuthUserBase(TKey id, string account, string name)
        {
            Id = id;
            Account = account;
            Name = name;
            exp = DateTime.Now.AddSeconds(AuthConfigProvider.AuthConfig.Expires).ToTimestamp();
        }

        /// <summary>
        /// 用户唯一标识 ID
        /// </summary>
        public TKey Id { get; set; }
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 登录名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 失效时间戳
        /// </summary>
        public long exp { get; protected set; }
    }
}
