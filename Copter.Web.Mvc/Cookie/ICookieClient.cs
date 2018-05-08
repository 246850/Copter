using System;
using System.Collections.Generic;
using System.Web;

namespace Copter.Web.Mvc.Cookie
{
    /// <summary>
    /// Web客户端 Cooike操作接口
    /// </summary>
    public interface ICookieClient
    {
        /// <summary>
        /// 默认失效时间 7200秒
        /// </summary>
        int Expires { get; }

        /// <summary>
        /// 设置Cookie - 默认失效时间
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <param name="filter">过滤，加解密等等</param>
        void SetCookie(string name, string value, Func<string, string> filter = null);

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresSeconds">失效时间 单位秒</param>
        /// <param name="filter">过滤，加解密等等</param>
        void SetCookie(string name, string value, int expiresSeconds, Func<string, string> filter = null);
        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="cookie">HttpCookie 项</param>
        void SetCookie(HttpCookie cookie);
        /// <summary>
        /// 设置cookie - 批量
        /// </summary>
        /// <param name="cookies">HttpCookie 集合</param>
        void SetCookie(IList<HttpCookie> cookies);
        /// <summary>
        /// 更新Cookie
        /// </summary>
        /// <param name="cookie"></param>
        void UpdateCookie(HttpCookie cookie);
        /// <summary>
        /// 更新Cookie
        /// </summary>
        void UpdateCookie(IList<HttpCookie> cookies);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="name">键</param>
        /// <returns>True | False</returns>
        bool Contains(string name);

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="filter">过滤，加解密等等</param>
        /// <returns>cookie字符串</returns>
        string GetCookie(string name, Func<string, string> filter = null);
        /// <summary>
        /// 获取cookie 并通过Json.Net序列化为.Net对象
        /// </summary>
        /// <typeparam name="T">类型 T</typeparam>
        /// <param name="name"></param>
        /// <param name="filter">过滤，加解密等等</param>
        /// <returns>cookie 对象</returns>
        T GetCookie<T>(string name, Func<string, string> filter = null) where T : class, new();
        /// <summary>
        /// 移除Cookie
        /// </summary>
        /// <param name="name">键</param>
        void Remove(string name);
        /// <summary>
        /// 移除Cookie
        /// </summary>
        /// <param name="cookie">Cookie 项</param>
        void Remove(HttpCookie cookie);
        /// <summary>
        /// 移除Cookie - 批量
        /// </summary>
        /// <param name="cookies">Cookie 集合</param>
        void Remove(IList<HttpCookie> cookies);
    }
}
