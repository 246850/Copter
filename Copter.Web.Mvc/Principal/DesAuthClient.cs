using System;
using Copter.Infrastructure.Configs;
using Copter.Infrastructure.Models;
using Copter.Infrastructure.Security;
using Copter.Infrastructure.Serialization;
using Copter.Web.Mvc.Cookie;
using Copter.Infrastructure.ValueObject;

namespace Copter.Web.Mvc.Principal
{
    /// <summary>
    /// Des加解密 授权客户端
    /// </summary>
    public class DesAuthClient<TBody, TKey> : IAuthClient<TBody, TKey> where TBody : AuthUserBase<TKey>, new()
    {
        public DesAuthClient() : this(new DefaultSecretBuilder("*&43Jyd&"))
        {

        }

        public DesAuthClient(ISecretBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException("builder");
            SecretBuilder = builder;
        }
        public ISecretBuilder SecretBuilder { get; }

        public void SignIn(TBody body)
        {
            //  获取 密钥
            string secret = SecretBuilder.Build();
            if (string.IsNullOrWhiteSpace(secret)) throw new Exception("应用程序密钥（AppSecret）为空或null");

            ICookieFactory cookieFactory = new CookieFactory();
            ICookieClient cookieClient = cookieFactory.Create();

            cookieClient.SetCookie(AuthConfigProvider.AuthConfig.CookieName, body.SerializeObject(), AuthConfigProvider.AuthConfig.Expires,
                value =>
                {
                    ICryptor cryptor = new DesCryptor(SecretBuilder.AppKey, secret);
                    return cryptor.Encrypt(value);
                });

        }

        public TBody GetBody()
        {
            //  获取 密钥
            string secret = SecretBuilder.Build();
            if (string.IsNullOrWhiteSpace(secret)) throw new Exception("应用程序密钥（AppSecret）为空或null");

            ICookieFactory cookieFactory = new CookieFactory();
            ICookieClient cookieClient = cookieFactory.Create();
            if (!cookieClient.Contains(AuthConfigProvider.AuthConfig.CookieName)) return null;

            string token = cookieClient.GetCookie(AuthConfigProvider.AuthConfig.CookieName, value =>
            {
                ICryptor cryptor = new DesCryptor(SecretBuilder.AppKey, secret);
                return cryptor.Decrypt(value);
            });

            TBody authUser = token.DeserializeObject<TBody>();

            DateTime expires = authUser.exp.AsDateTime();
            if (expires < DateTime.Now) return null;    // 已失效
            
            SignIn(authUser);

            return authUser;
        }

        public void SignOut()
        {
            ICookieFactory cookieFactory = new CookieFactory();
            ICookieClient cookieClient = cookieFactory.Create();
            cookieClient.Remove(AuthConfigProvider.AuthConfig.CookieName);
        }
    }
}
