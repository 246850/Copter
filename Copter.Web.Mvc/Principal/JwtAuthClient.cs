using Copter.Web.Mvc.Cookie;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using Copter.Infrastructure.Configs;
using Copter.Infrastructure.Models;

namespace Copter.Web.Mvc.Principal
{
    /// <summary>
    /// Jwt加解密 授权客户端
    /// </summary>
    public class JwtAuthClient<TBody, TKey> : IAuthClient<TBody, TKey> where TBody : AuthUserBase<TKey>, new()
    {
        /// <summary>
        /// 默认采用 DefaultSecretBuilder
        /// </summary>
        public JwtAuthClient() : this(new DefaultSecretBuilder())
        {

        }

        public JwtAuthClient(ISecretBuilder builder)
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

            //  生成加密token;
            IAlgorithmFactory algorithmFactory = new HMACSHAAlgorithmFactory();
            IJwtAlgorithm algorithm = algorithmFactory.Create(AuthConfigProvider.AuthConfig.JwtAlgorithmType);
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            string token = encoder.Encode(body, secret);

            //  写入Cookie
            ICookieFactory cookieFactory = new CookieFactory();
            ICookieClient cookieClient = cookieFactory.Create();
            cookieClient.SetCookie(AuthConfigProvider.AuthConfig.CookieName, token, AuthConfigProvider.AuthConfig.Expires);
        }

        public TBody GetBody()
        {
            //  获取 密钥
            string secret = SecretBuilder.Build();
            if (string.IsNullOrWhiteSpace(secret)) throw new Exception("应用程序密钥（AppSecret）为空或null");

            ICookieFactory cookieFactory = new CookieFactory();
            ICookieClient cookieClient = cookieFactory.Create();
            if (!cookieClient.Contains(AuthConfigProvider.AuthConfig.CookieName)) return null;

            // 获取cookie, 并解密 数据
            string token = cookieClient.GetCookie(AuthConfigProvider.AuthConfig.CookieName);
            IAlgorithmFactory algorithmFactory = new HMACSHAAlgorithmFactory();
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithmFactory);
            TBody authUser = decoder.DecodeToObject<TBody>(token, secret, true);
            
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
