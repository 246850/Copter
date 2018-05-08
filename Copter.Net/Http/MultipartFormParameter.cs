using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Copter.Net.Http
{
    /// <summary>
    /// 上传文件 - 请求参数类
    /// </summary>
    public class MultipartFormParameter
    {
        public MultipartFormParameter()
        {
            FileNameKey = "fileName";
            Encoding = Encoding.UTF8;
            Parameters = new Dictionary<string, string>();
            Method = HttpMethodType.Post;
        }
        /// <summary>
        /// 上传地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 文件名称key
        /// </summary>
        public string FileNameKey { get; set; }
        /// <summary>
        /// 文件名称value
        /// </summary>
        public string FileNameValue { get; set; }
        /// <summary>
        /// 编码格式
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// 上传文件的流
        /// </summary>
        public Stream UploadStream { get; set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        public HttpMethodType Method { get; set; }
        /// <summary>
        /// 上传文件 携带的参数集合
        /// </summary>
        public IDictionary<string, string> Parameters { get; set; }
    }
}
