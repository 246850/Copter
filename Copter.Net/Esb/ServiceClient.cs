using System;
using System.Collections.Specialized;
using System.Net.Http;
using Copter.Infrastructure.MessageHandler;
using Copter.Infrastructure.Serialization;

namespace Copter.Net.Esb
{
    /// <summary>
    /// 默认携带Token Http接口请求 工具类
    /// </summary>
    public sealed class ServiceClient
    {
        private static readonly Lazy<IEsbClient> LazyClient = new Lazy<IEsbClient>(() => new ESBClient(new HttpClient(new Sha1AuthorizationMessageHandler())));

        #region - Post
        /// <summary>
        /// Http协议POST请求 - ContentType:application/json
        /// </summary>
        /// <typeparam name="TRes">响应对象类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <returns>类型T结果对象</returns>
        public static TRes Post<TRes>(string url)
        {
            return Post<TRes>(url, string.Empty);
        }

        /// <summary>
        /// Http协议POST请求 - ContentType:application/json
        /// </summary>
        /// <typeparam name="TRes">响应对象类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="data">参数对象</param>
        /// <returns>类型T结果对象</returns>
        public static TRes Post<TRes>(string url, string data)
        {
            NameValueCollection keyValues = new NameValueCollection
            { { "Accept", "application/json" }};
            return Post<TRes>(url, data, keyValues);
        }

        /// <summary>
        /// Http协议POST请求
        /// </summary>
        /// <typeparam name="TRes">响应对象类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="data">参数对象</param>
        /// <param name="headers">自定义请求头 集合</param>
        /// <returns>类型T结果对象</returns>
        public static TRes Post<TRes>(string url, string data, NameValueCollection headers)
        {
            return Post(url, data, headers).DeserializeObject<TRes>();
        }

        /// <summary>
        /// Http协议POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">参数对象</param>
        /// <param name="headers">自定义请求头 集合</param>
        /// <returns>响应结果 字符串</returns>
        public static string Post(string url, string data, NameValueCollection headers)
        {
            return LazyClient.Value.Post(url, data, headers);
        }

        /// <summary>
        /// Http协议POST请求 - ContentType:application/json
        /// </summary>
        /// <typeparam name="TRes">响应对象类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="data">参数对象</param>
        /// <returns>类型T结果对象</returns>
        public static TRes Post<TRes>(string url, object data)
        {
            return Post<TRes>(url, data.SerializeObject());
        }
        #endregion

        #region - GET

        /// <summary>
        /// Http协议GET请求
        /// </summary>
        /// <typeparam name="TRes">响应对象类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <returns>类型T结果对象</returns>
        public static TRes Get<TRes>(string url)
        {
            return Get<TRes>(url, null, null);
        }

        /// <summary>
        /// Http协议GET请求
        /// </summary>
        /// <typeparam name="TRes">响应对象类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="queryStrings">queryString 参数集合</param>
        /// <param name="headers">自定义请求头 集合</param>
        /// <returns>类型T结果对象</returns>
        public static TRes Get<TRes>(string url, NameValueCollection queryStrings, NameValueCollection headers)
        {
            return Get(url, queryStrings, headers).DeserializeObject<TRes>();
        }

        /// <summary>
        /// Http协议GET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="queryStrings">queryString 参数集合</param>
        /// <param name="headers">自定义请求头 集合</param>
        /// <returns>响应结果 字符串</returns>
        public static string Get(string url, NameValueCollection queryStrings, NameValueCollection headers)
        {
            return LazyClient.Value.Get(url, queryStrings, headers);
        }
        #endregion
    }
}
