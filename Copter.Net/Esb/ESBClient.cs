using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using Copter.Infrastructure.Serialization;
using System;

namespace Copter.Net.Esb
{
    /// <summary>
    /// 通信组件接口实现工具类  - 基于Json格式数据
    /// </summary>
    public class ESBClient : IEsbClient
    {
        public ESBClient():this(new HttpClient())
        {

        }

        public ESBClient(HttpClient client)
        {
            Client = client;
        }
        public HttpClient Client { get; }

        #region - POST

        /// <summary>
        /// Http协议POST请求 - ContentType:application/json
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">参数对象</param>
        public void Post(string url, string data)
        {
            Post(url, data, null);
        }
        /// <summary>
        /// Http协议POST请求 - ContentType:application/json
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">参数对象</param>
        public void Post(string url, object data)
        {
            Post(url, data.SerializeObject());
        }

        /// <summary>
        /// Http协议POST请求 - ContentType:application/json
        /// </summary>
        /// <typeparam name="TRes">响应对象类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <returns>类型T结果对象</returns>
        public TRes Post<TRes>(string url)
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
        public TRes Post<TRes>(string url, string data)
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
        public TRes Post<TRes>(string url, string data, NameValueCollection headers)
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
        public string Post(string url, string data, NameValueCollection headers)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(data, Encoding.UTF8, "application/json")
            };
            if (headers != null)
            {
                foreach (string name in headers)
                {
                    request.Headers.Add(name, headers[name]);
                }
            }

            HttpResponseMessage response = Send(request);
            string content = response.Content.ReadAsStringAsync().Result;
            return content;
        }

        /// <summary>
        /// Http协议POST请求 - ContentType:application/json
        /// </summary>
        /// <typeparam name="TRes">响应对象类型</typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="data">参数对象</param>
        /// <returns>类型T结果对象</returns>
        public TRes Post<TRes>(string url, object data)
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
        public TRes Get<TRes>(string url)
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
        public TRes Get<TRes>(string url, NameValueCollection queryStrings, NameValueCollection headers)
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
        public string Get(string url, NameValueCollection queryStrings, NameValueCollection headers)
        {
            string query = string.Empty;
            if (queryStrings != null)
            {
                query = string.Join("&", queryStrings.AllKeys.Select(key => string.Format("{0}={1}", key, queryStrings[key])));
            }
            if (string.IsNullOrWhiteSpace(query))
            {
                url = string.Concat(url, !url.Contains("?") ? "?" : "&", query);
            }
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            if (headers != null)
            {
                foreach (string name in headers)
                {
                    request.Headers.Add(name, headers[name]);
                }
            }

            HttpResponseMessage response = Send(request);
            string content = response.Content.ReadAsStringAsync().Result;
            return content;
        }
        #endregion

        /// <summary>
        /// Http协议请求
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <returns>响应信息</returns>
        public HttpResponseMessage Send(HttpRequestMessage request)
        {
            try
            {
                HttpResponseMessage response = Client.SendAsync(request).Result;
                
                response.EnsureSuccessStatusCode();
                return response;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
