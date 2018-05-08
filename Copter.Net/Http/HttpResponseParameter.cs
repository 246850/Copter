using System;
using System.Net;

namespace Copter.Net.Http
{
    /// <summary>
    /// Http响应 参数类
    /// </summary>
    public class HttpResponseParameter
    {
        public HttpResponseParameter()
        {
            Cookie = new HttpCookier();
        }
        /// <summary>
        /// 响应资源标识符
        /// </summary>
        public Uri Uri { get; set; }
        /// <summary>
        /// 响应状态码
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }
        /// <summary>
        /// 响应Cookie对象
        /// </summary>
        public HttpCookier Cookie { get; set; }
        /// <summary>
        /// 响应长度
        /// </summary>
        public long ContentLength { get; set; }
        /// <summary>
        /// 响应类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 响应体
        /// </summary>
        public string Body { get; set; }
    }
}
