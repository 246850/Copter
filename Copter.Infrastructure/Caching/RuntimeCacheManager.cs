using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;

namespace Copter.Infrastructure.Caching
{
    /// <summary>
    /// HttpRuntime Cache工具类
    /// </summary>
    public class RuntimeCacheManager : ICacheManager
    {
        public RuntimeCacheManager():this(HttpRuntime.Cache)
        {
            
        }
        public RuntimeCacheManager(System.Web.Caching.Cache cache)
        {
            if (cache == null) throw new ArgumentNullException("cache");
            RuntimeCache = cache;
        }
        protected System.Web.Caching.Cache RuntimeCache { get; }

        #region 实现
        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("缓存键key不能为空或null", "key");
            if (Contains(key))
                return (T)RuntimeCache[key];
            return default(T);
        }

        public T Get<T>(string key, Action func)
        {
            if (func != null) func.Invoke();
            return Get<T>(key);
        }

        public void Set(string key, object data)
        {
            Set(key, data, InternalCacheConst.DefaultExpirate);
        }

        public void Set(string key, object data, int expirate)
        {
            if (data == null) throw new ArgumentException("缓存值data不能为空或null", "data");
            RuntimeCache.Insert(key, data, null, DateTime.Now.AddSeconds(expirate), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        public bool Contains(string key)
        {
            return RuntimeCache.Get(key) != null;
        }

        public void Remove(string key)
        {
            if (Contains(key)) RuntimeCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (KeyValuePair<string, object> keyValuePair in RuntimeCache)
            {
                if (regex.IsMatch(keyValuePair.Key))
                    Remove(keyValuePair.Key);
            }
        }

        public void Clear()
        {
            foreach (KeyValuePair<string, object> keyValuePair in RuntimeCache)
                Remove(keyValuePair.Key);
        } 
        #endregion
    }
}
