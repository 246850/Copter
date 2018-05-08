using System.Collections.Specialized;
using System.Net.Http;

namespace Copter.Net.Esb
{
    /// <summary>
    /// 通信组件接口
    /// </summary>
    public interface IEsbClient
    {
        /// <summary>
        /// Http请求 客户端。注：采用单例模式
        /// </summary>
        HttpClient Client { get; }

        void Post(string url, string data);

        void Post(string url, object data);

        TRes Post<TRes>(string url);

        TRes Post<TRes>(string url, string data);

        TRes Post<TRes>(string url, string data, NameValueCollection headers);
        string Post(string url, string data, NameValueCollection headers);

        TRes Post<TRes>(string url, object data);
        TRes Get<TRes>(string url);
        TRes Get<TRes>(string url, NameValueCollection queryStrings, NameValueCollection headers);
        string Get(string url, NameValueCollection queryStrings, NameValueCollection headers);
    }
}
