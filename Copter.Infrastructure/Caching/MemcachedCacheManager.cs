using System;

namespace Copter.Infrastructure.Caching
{
    /// <summary>
    /// Memeched Cache工具类
    /// </summary>
    public class MemcachedCacheManager : ICacheManager
    {
        #region 实现
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key, Action func)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object data)
        {
            throw new NotImplementedException();
        }

        public void Set(string key, object data, int expirate)
        {
            throw new NotImplementedException();
        } 
        #endregion
    }
}
