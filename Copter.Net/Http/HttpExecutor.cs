using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Copter.Net.Http
{
    /// <summary>
    /// Http请求最终执行者 - HttpWebRequest
    /// </summary>
    internal class HttpExecutor
    {
        public static HttpResponseParameter Execute(HttpRequestParameter requestParameter)
        {
            // 1.实例化 - 设置请求行
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(requestParameter.Url, UriKind.RelativeOrAbsolute));
            webRequest.Method = requestParameter.Method.ToString();
            webRequest.ProtocolVersion = requestParameter.HttpVersion;

            if (Regex.IsMatch(requestParameter.Url, "^https://"))
            {
                // .ssl/https请求设置
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
            }

            // 2.设置请求头
            SetHeader(webRequest, requestParameter);

            // 3.设置请求Cookie
            SetCookie(webRequest, requestParameter);

            // 4.设置请求代理
            SetProxy(webRequest, requestParameter);

            // 5.设置请求参数[Post方式下]
            SetBody(webRequest, requestParameter);

            return GetResponse(webRequest, requestParameter);
        }

        /// <summary>
        /// 设置请求头
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        static void SetHeader(HttpWebRequest webRequest, HttpRequestParameter requestParameter)
        {
            webRequest.Accept = requestParameter.Accept;
            webRequest.ContentType = requestParameter.ContentType;
            webRequest.UserAgent = requestParameter.UserAgent;
            webRequest.Referer = requestParameter.Referer;
            webRequest.KeepAlive = requestParameter.KeepAlive;
            webRequest.Timeout = requestParameter.Timeout;

            if (!string.IsNullOrEmpty(requestParameter.Origin))
            {
                // 自定义请求头
                webRequest.Headers["Origin"] = requestParameter.Origin;
            }
        }

        /// <summary>
        /// 设置请求 cookie
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        static void SetCookie(HttpWebRequest webRequest, HttpRequestParameter requestParameter)
        {
            // 必须实例化cookie容器，以获取响应中的cookie，否则获取不到
            webRequest.CookieContainer = new CookieContainer();
            if (requestParameter.Cookie != null)
            {
                if (!string.IsNullOrEmpty(requestParameter.Cookie.CookieString))
                {
                    webRequest.Headers[HttpRequestHeader.Cookie] = requestParameter.Cookie.CookieString;
                }

                if (requestParameter.Cookie.CookieCollection != null &&
                    requestParameter.Cookie.CookieCollection.Count > 0)
                {
                    webRequest.CookieContainer.Add(requestParameter.Cookie.CookieCollection);
                }
            }
        }

        /// <summary>
        /// 设置请求 代理
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        static void SetProxy(HttpWebRequest webRequest, HttpRequestParameter requestParameter)
        {
            if (requestParameter.IsProxy && !string.IsNullOrEmpty(requestParameter.ProxyIp))
            {
                webRequest.Proxy = new WebProxy(requestParameter.ProxyIp, true);
                webRequest.Credentials = CredentialCache.DefaultCredentials;
            }
        }

        /// <summary>
        /// 设置请求 体
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        static void SetBody(HttpWebRequest webRequest, HttpRequestParameter requestParameter)
        {
            if (requestParameter.Method != HttpMethodType.Get)
            {
                string para;
                if (requestParameter.Parameters != null && requestParameter.Parameters.Count > 0)
                {
                    StringBuilder data = new StringBuilder(string.Empty);
                    foreach (KeyValuePair<string, string> keyValuePair in requestParameter.Parameters)
                    {
                        data.AppendFormat("{0}={1}&", keyValuePair.Key, HttpUtility.UrlEncode(keyValuePair.Value));
                    }
                    para = data.Remove(data.Length - 1, 1).ToString();
                }
                else
                {
                    para = requestParameter.PostData;
                }
                if (string.IsNullOrEmpty(para))
                {
                    para = string.Empty;
                }
                byte[] bytePosts = requestParameter.Encoding.GetBytes(para);
                webRequest.ContentLength = bytePosts.Length;
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(bytePosts, 0, bytePosts.Length);
                    requestStream.Close();
                }
            }
        }

        /// <summary>
        /// 获取响应内容 | 或者操作响应流
        /// </summary>
        /// <param name="webRequest">HttpWebRequest对象</param>
        /// <param name="requestParameter">请求参数对象</param>
        static HttpResponseParameter GetResponse(HttpWebRequest webRequest, HttpRequestParameter requestParameter)
        {
            HttpResponseParameter responseParameter = new HttpResponseParameter();
            try
            {
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    responseParameter.Uri = webResponse.ResponseUri;
                    responseParameter.StatusCode = webResponse.StatusCode;
                    responseParameter.Cookie = new HttpCookier
                    {
                        CookieCollection = webResponse.Cookies,
                        CookieString = webResponse.Headers[HttpResponseHeader.SetCookie]
                    };
                    responseParameter.ContentType = webResponse.ContentType;
                    responseParameter.ContentLength = webResponse.ContentLength;
                    if (requestParameter.ResponseType == HttpResponseType.String)
                    {
                        using (StreamReader reader = new StreamReader(webResponse.GetResponseStream(), requestParameter.Encoding))
                        {
                            responseParameter.Body = reader.ReadToEnd();
                            reader.Close();
                        }
                    }
                    else
                    {
                        if (requestParameter.StreamAction != null)
                        {
                            requestParameter.StreamAction(webResponse.GetResponseStream());
                        }
                    }
                    webResponse.Close();
                }
            }
            catch (WebException ex)
            {
                using (StreamReader reader = new StreamReader(ex.Response.GetResponseStream(), requestParameter.Encoding))
                {
                    responseParameter.Body = reader.ReadToEnd();
                }
            }

            return responseParameter;
        }

        static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }
    }
}
