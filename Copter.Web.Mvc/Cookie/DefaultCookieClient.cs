using System;
using System.Collections.Generic;
using System.Web;
using Copter.Infrastructure.Serialization;

namespace Copter.Web.Mvc.Cookie
{
    internal class DefaultCookieClient : ICookieClient
    {
        public int Expires
        {
            get { return 7200; }
        }

        public void SetCookie(string name, string value, Func<string, string> filter = null)
        {
            SetCookie(name, value, Expires, filter);
        }

        public void SetCookie(string name, string value, int expiresSeconds, Func<string, string> filter = null)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("name");
            if (filter != null) value = filter(value);  //过滤 加解密等等
            HttpCookie cookie = new HttpCookie(name, value)
            {
                Expires = DateTime.Now.AddSeconds(expiresSeconds),
                HttpOnly = true
            };
            SetCookie(cookie);
        }

        public void SetCookie(HttpCookie cookie)
        {
            if (cookie == null)
                throw new ArgumentNullException("cookie");
            IList<HttpCookie> cookies = new List<HttpCookie>();
            cookies.Add(cookie);
            SetCookie(cookies);
        }

        public void SetCookie(IList<HttpCookie> cookies)
        {
            if (HttpContext.Current == null)
                throw new NullReferenceException("HttpContext.Current为null！");
            if (cookies == null)
                throw new ArgumentNullException("cookies");
            foreach (HttpCookie item in cookies)
                HttpContext.Current.Response.SetCookie(item);
        }

        public bool Contains(string name)
        {
            return !string.IsNullOrWhiteSpace(GetCookie(name));
        }

        public string GetCookie(string name, Func<string, string> filter = null)
        {
            if (HttpContext.Current == null || HttpContext.Current.Request.Cookies[name] == null)
                return string.Empty;
            string result = HttpContext.Current.Request.Cookies[name].Value;
            if (filter != null) result = filter(result);
            return result;
        }

        public T GetCookie<T>(string name, Func<string, string> filter = null) where T : class, new()
        {
            if (!Contains(name)) throw new Exception("cookie不存在");
            string value = GetCookie(name, filter);
            return value.DeserializeObject<T>();
        }

        public void Remove(string name)
        {
            HttpCookie cookie = new HttpCookie(name);
            Remove(cookie);
        }

        public void Remove(HttpCookie cookie)
        {
            if (cookie == null)
                throw new ArgumentNullException("cookie", "清除cookie不能为null");
            IList<HttpCookie> cookies = new List<HttpCookie>();
            cookies.Add(cookie);
            Remove(cookies);
        }

        public void Remove(IList<HttpCookie> cookies)
        {
            if (cookies == null)
                throw new ArgumentNullException("cookies", "清除cookie不能为null");
            foreach (HttpCookie item in cookies)
            {
                item.Expires = DateTime.Now.AddDays(-30);
                HttpContext.Current.Response.SetCookie(item);
            }
        }

        public void UpdateCookie(HttpCookie cookie)
        {
            if (cookie == null)
                throw new ArgumentNullException("cookie", "更新cookie不能为null");
            IList<HttpCookie> cookies = new List<HttpCookie>();
            cookies.Add(cookie);
            UpdateCookie(cookies);
        }

        public void UpdateCookie(IList<HttpCookie> cookies)
        {
            if (HttpContext.Current == null)
                throw new NullReferenceException("HttpContext.Current为null！");
            if (cookies == null)
                throw new ArgumentNullException("cookies", "更新cookie不能为null");
            foreach (HttpCookie item in cookies)
                HttpContext.Current.Response.SetCookie(item);
        }
    }
}
