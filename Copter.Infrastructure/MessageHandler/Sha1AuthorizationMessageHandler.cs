using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Copter.Infrastructure.Models;
using System;
using Copter.Infrastructure.Generator;
using Copter.Infrastructure.ValueObject;
using System.Collections.Generic;
using Copter.Infrastructure.Security;
using Copter.Infrastructure.Serialization;

namespace Copter.Infrastructure.MessageHandler
{
    /// <summary>
    /// 数据加密/解密 | 授权验证 消息处理Handler
    /// </summary>
    public class Sha1AuthorizationMessageHandler : MessageProcessingHandler
    {
        public Sha1AuthorizationMessageHandler():this(new HttpClientHandler())
        {
            
        }
        public Sha1AuthorizationMessageHandler(HttpMessageHandler innerHandler) :base(innerHandler)
        {
            
        }
        protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", BuildAuthToken(DefaultSecretConst.AppKey));
            return request;
        }

        protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            return response;
        }

        /// <summary>
        /// 生成授权token
        /// </summary>
        /// <returns>授权token</returns>
        string BuildAuthToken(string appKey)
        {
            string nonce = NonceGenerator.GenerateString(),
                appSecret = GetAppSecret(appKey);
            long timestamp = DateTime.Now.ToTimestamp();

            // Sh1加密
            List<string> list = new List<string>()
            {
                nonce,
                appSecret,
                timestamp.ToString()
            };
            // 字典排序
            list.Sort();
            ICryptor cryptor = new Sha1Cryptor();
            string signature = cryptor.Encrypt(string.Join(string.Empty, list));

            AuthParameterModel auth = new AuthParameterModel
            {
                AppKey = appKey,
                Nonce = nonce,
                Timestamp = timestamp,
                Signature = signature
            };
            string authJson = auth.SerializeObject(),
                token = authJson.ToBase64();
            return token;
        }

        /// <summary>
        /// 获取应用程序 密钥
        /// </summary>
        /// <param name="appKey">应用程序唯一标识 key</param>
        /// <returns>密钥</returns>
        string GetAppSecret(string appKey)
        {
            if (string.Equals(appKey, DefaultSecretConst.AppKey, StringComparison.OrdinalIgnoreCase))
            {
                return DefaultSecretConst.AppSecret;
            }
            return string.Empty;
        }
    }
}
