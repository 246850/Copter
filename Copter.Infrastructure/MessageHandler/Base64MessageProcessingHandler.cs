using System.Net.Http;
using System.Text;
using System.Threading;
using Copter.Infrastructure.ValueObject;

namespace Copter.Infrastructure.MessageHandler
{
    /// <summary>
    /// 对请求和/或响应消息进行一些小型处理的处理程序拦截器 - Base64加解密。
    /// </summary>
    public class Base64MessageProcessingHandler : MessageProcessingHandler
    {
        /// <summary>
        /// 处理请求 Base64解密
        /// </summary>
        protected override HttpRequestMessage ProcessRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string body = request.Content.ReadAsStringAsync().Result;
            if (string.IsNullOrWhiteSpace(body)) return request;
            try
            {
                string charSet = request.Content.Headers.ContentType.CharSet ?? "utf-8";
                request.Content = new StringContent(body.DeBase64(), Encoding.GetEncoding(charSet), request.Content.Headers.ContentType.MediaType);
            }
            catch
            {
                // ignored
            }
            return request;
        }

        protected override HttpResponseMessage ProcessResponse(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (!response.IsSuccessStatusCode) return response;
            string body = response.Content.ReadAsStringAsync().Result;
            
            if (string.IsNullOrWhiteSpace(body)) return response;
            response.Content = new StringContent(body.ToBase64(), Encoding.GetEncoding(response.Content.Headers.ContentType.CharSet),response.Content.Headers.ContentType.MediaType);
            return response;
        }
    }
}
