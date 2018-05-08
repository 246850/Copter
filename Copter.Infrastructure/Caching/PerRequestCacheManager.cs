using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace Copter.Infrastructure.Caching
{
    /// <summary>
    /// HttpContext.Items Cache 工具类
    /// </summary>
    public class PerRequestCacheManager : ICacheManager
    {
        protected virtual HttpContext Context{get;}
        public PerRequestCacheManager():this(HttpContext.Current)
        {

        }
        public PerRequestCacheManager(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            Context = context;
        }

        #region 实现
        public void Clear()
        {
            Context.Items.Clear();
        }

        public bool Contains(string key)
        {
            return Context.Items.Contains(key);
        }

        public T Get<T>(string key)
        {
            if (!Contains(key)) return default(T);

            return (T)Context.Items[key];
        }

        public T Get<T>(string key, Action func)
        {
            if (func != null) func.Invoke();
            return Get<T>(key);
        }

        public void Remove(string key)
        {
            if (Contains(key)) Context.Items.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (KeyValuePair<string, object> keyValuePair in Context.Items)
            {
                if (regex.IsMatch(keyValuePair.Key))
                    Remove(keyValuePair.Key);
            }
        }

        public void Set(string key, object data)
        {
            Set(key, data, InternalCacheConst.DefaultExpirate);
        }

        public void Set(string key, object data, int expirate)
        {
            if (data == null) throw new ArgumentException("缓存值data不能为空或null", "data");
            Context.Items[key] = data;
        } 
        #endregion
    }
}
