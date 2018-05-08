using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace Copter.Infrastructure.Caching
{
    /// <summary>
    /// MemoryCache 工具类
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {
        public MemoryCacheManager() : this(MemoryCache.Default)
        {

        }

        public MemoryCacheManager(ObjectCache cache)
        {
            if (cache == null) throw new ArgumentNullException("cache");
            ObjectCache = cache;
        }
        protected ObjectCache ObjectCache { get; }

        #region 实现
        public T Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("缓存键key不能为空或null", "key");
            if (ObjectCache.Contains(key))
                return (T)ObjectCache[key];
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
            if (string.IsNullOrWhiteSpace(key)) throw new ArgumentException("缓存键key不能为空或null", "key");
            if (data == null) throw new ArgumentException("缓存值data不能为空或null", "data");

            CacheItemPolicy policy = new CacheItemPolicy
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(expirate)
            };
            ObjectCache.Add(new CacheItem(key, data), policy);
        }

        public bool Contains(string key)
        {
            return ObjectCache.Contains(key);
        }

        public void Remove(string key)
        {
            if (Contains(key)) ObjectCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {
            Regex regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            foreach (KeyValuePair<string, object> keyValuePair in ObjectCache)
            {
                if (regex.IsMatch(keyValuePair.Key))
                    Remove(keyValuePair.Key);
            }
        }

        public void Clear()
        {
            foreach (KeyValuePair<string, object> keyValuePair in ObjectCache)
                Remove(keyValuePair.Key);
        } 
        #endregion
    }
}
