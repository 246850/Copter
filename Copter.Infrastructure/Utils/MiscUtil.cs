using System.Web;
using Copter.Infrastructure.ValueObject;
using System;
using System.Net;

namespace Copter.Infrastructure.Utils
{
    /// <summary>
    /// 获取本机或客户端信息 工具类
    /// </summary>
    public sealed class MiscUtil
    {
        /// <summary>
        /// 获取远程客户端的真实IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetRemoteIp()
        {
            string local = "127.0.0.1";
            if (HttpContext.Current == null) return local;

            string result = GetIpAddr(HttpContext.Current.Request);
            if (!result.IsIpAddress())
            {
                result = local;
            }
            return result;
        }
        static string GetIpAddr(HttpRequest request)
        {
            //HTTP_X_FORWARDED_FOR
            string ipAddress = request.ServerVariables["x-forwarded-for"];
            if (!IsEffectiveIP(ipAddress))
            {
                ipAddress = request.ServerVariables["Proxy-Client-IP"];
            }
            if (!IsEffectiveIP(ipAddress))
            {
                ipAddress = request.ServerVariables["WL-Proxy-Client-IP"];
            }
            if (!IsEffectiveIP(ipAddress))
            {
                ipAddress = request.ServerVariables["Remote_Addr"];
                if (ipAddress.Equals("127.0.0.1") || ipAddress.Equals("::1"))
                {
                    // 根据网卡取本机配置的IP
                    IPAddress[] AddressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                    foreach (IPAddress _IPAddress in AddressList)
                    {
                        if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                        {
                            ipAddress = _IPAddress.ToString();
                            break;
                        }
                    }
                }
            }
            // 对于通过多个代理的情况，第一个IP为客户端真实IP,多个IP按照','分割
            if (ipAddress != null && ipAddress.Length > 15)
            {
                if (ipAddress.IndexOf(",") > 0)
                {
                    ipAddress = ipAddress.Substring(0, ipAddress.IndexOf(","));
                }
            }
            return ipAddress;
        }

        /// <summary>
        /// 是否有效IP地址
        /// </summary>
        /// <param name="ipAddress">IP地址</param>
        /// <returns>bool</returns>
        static bool IsEffectiveIP(string ipAddress)
        {
            return !(string.IsNullOrEmpty(ipAddress) || "unknown".Equals(ipAddress, StringComparison.OrdinalIgnoreCase));
        }
    }
}
