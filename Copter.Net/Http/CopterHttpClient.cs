using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Copter.Infrastructure.Serialization;

namespace Copter.Net.Http
{
    /// <summary>
    /// HttpWebRequest http请求 封装类
    /// </summary>
    public sealed class CopterHttpClient
    {
        public static HttpResponseParameter Execute(HttpRequestParameter request)
        {
            return HttpExecutor.Execute(request);
        }

        public static string Get(HttpRequestParameter request)
        {
            request.Method = HttpMethodType.Get;
            HttpResponseParameter response = Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Body;
            }
            return string.Empty;
        }

        public static string Post(HttpRequestParameter request)
        {
            request.Method = HttpMethodType.Post;
            HttpResponseParameter response = Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Body;
            }
            return string.Empty;
        }

        public static T Get<T>(HttpRequestParameter request) where T : class, new()
        {
            string result = Get(request);
            return !string.IsNullOrWhiteSpace(result) ? result.DeserializeObject<T>() : null;
        }

        public static T Post<T>(HttpRequestParameter request) where T : class, new()
        {
            string result = Post(request);
            return !string.IsNullOrWhiteSpace(result) ? result.DeserializeObject<T>() : null;
        }

        static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        public static string MultipartFormRequest(MultipartFormParameter parameter)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // 1.分界线
                string boundary = string.Format("----{0}", DateTime.Now.Ticks.ToString("x")),       // 分界线可以自定义参数
                    beginBoundary = string.Format("--{0}\r\n", boundary),
                    endBoundary = string.Format("\r\n--{0}--\r\n", boundary);
                byte[] beginBoundaryBytes = parameter.Encoding.GetBytes(beginBoundary),
                    endBoundaryBytes = parameter.Encoding.GetBytes(endBoundary);
                // 2.组装开始分界线数据体 到内存流中
                memoryStream.Write(beginBoundaryBytes, 0, beginBoundaryBytes.Length);
                // 3.组装 上传文件附加携带的参数 到内存流中
                if (parameter.Parameters != null && parameter.Parameters.Count > 0)
                {
                    foreach (KeyValuePair<string, string> keyValuePair in parameter.Parameters)
                    {
                        string parameterHeaderTemplate = string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}\r\n{2}", keyValuePair.Key, keyValuePair.Value, beginBoundary);
                        byte[] parameterHeaderBytes = parameter.Encoding.GetBytes(parameterHeaderTemplate);

                        memoryStream.Write(parameterHeaderBytes, 0, parameterHeaderBytes.Length);
                    }
                }
                // 4.组装文件头数据体 到内存流中
                string fileHeaderTemplate = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n", parameter.FileNameKey, parameter.FileNameValue);
                byte[] fileHeaderBytes = parameter.Encoding.GetBytes(fileHeaderTemplate);
                memoryStream.Write(fileHeaderBytes, 0, fileHeaderBytes.Length);
                // 5.组装文件流 到内存流中
                byte[] buffer = new byte[1024 * 1024 * 1];
                int size = parameter.UploadStream.Read(buffer, 0, buffer.Length);
                while (size > 0)
                {
                    memoryStream.Write(buffer, 0, size);
                    size = parameter.UploadStream.Read(buffer, 0, buffer.Length);
                }
                // 6.组装结束分界线数据体 到内存流中
                memoryStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                // 7.获取二进制数据
                byte[] postBytes = memoryStream.ToArray();
                // 8.HttpWebRequest 组装
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(parameter.Url, UriKind.RelativeOrAbsolute));
                webRequest.Method = parameter.Method.ToString().ToUpper();
                webRequest.Timeout = 10000;
                webRequest.ContentType = string.Format("multipart/form-data; boundary={0}", boundary);
                webRequest.ContentLength = postBytes.Length;
                if (Regex.IsMatch(parameter.Url, "^https://"))
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
                }
                // 9.写入上传请求数据
                using (Stream requestStream = webRequest.GetRequestStream())
                {
                    requestStream.Write(postBytes, 0, postBytes.Length);
                    requestStream.Close();
                }
                // 10.获取响应
                using (HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(webResponse.GetResponseStream(), parameter.Encoding))
                    {
                        string body = reader.ReadToEnd();
                        reader.Close();
                        return body;
                    }
                }
            }

        }

        public static T MultipartFormRequest<T>(MultipartFormParameter parameter) where T : class, new()
        {
            string body = MultipartFormRequest(parameter);
            T result = body.DeserializeObject<T>();
            return result;
        }

        public static void MutipartFormRequest(MultipartFormParameter parameter)
        {
            MultipartFormRequest(parameter);
        }
    }
}
