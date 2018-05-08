using System;
using System.Text;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Copter.Infrastructure.Caching
{
    /// <summary>
    /// Redis缓存 工具类 应确保IConnectionMultiplexer只有一个实例
    /// </summary>
    public class RedisCacheManager : ICacheManager
    {
        public RedisCacheManager(IRedisConnectionWrapper connectionWrapper)
        {
            ConnectionWrapper = connectionWrapper;
            Db = ConnectionWrapper.GetDatabase();
        }
        /// <summary>
        /// Redis 连接池
        /// </summary>
        protected IRedisConnectionWrapper ConnectionWrapper { get; }
        /// <summary>
        /// Redis 连接数据库
        /// </summary>
        protected IDatabase Db { get; }

        protected virtual byte[] Serialize(object item)
        {
            string jsonString = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }
        protected virtual T Deserialize<T>(byte[] serializedObject)
        {
            if (serializedObject == null)
                return default(T);

            string jsonString = Encoding.UTF8.GetString(serializedObject);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        #region 实现
        public void Clear()
        {
            foreach (var ep in ConnectionWrapper.GetEndpoints())
            {
                var server = ConnectionWrapper.Server(ep);
                //we can use the code below (commented)
                //but it requires administration permission - ",allowAdmin=true"
                //server.FlushDatabase();

                //that's why we simply interate through all elements now
                var keys = server.Keys();
                foreach (var key in keys)
                    Db.KeyDelete(key);
            }
        }

        public bool Contains(string key)
        {
            return Db.KeyExists(key);
        }

        public T Get<T>(string key)
        {
            if (!Contains(key)) return default(T);

            RedisValue rValue = Db.StringGet(key);
            if (!rValue.HasValue) return default(T);

            T result = Deserialize<T>(rValue);
            return result;
        }

        public T Get<T>(string key, Action func)
        {
            if (func != null) func.Invoke();
            return Get<T>(key);
        }

        public void Remove(string key)
        {
            Db.KeyDelete(key);
        }

        public void RemoveByPattern(string pattern)
        {
            foreach (var ep in ConnectionWrapper.GetEndpoints())
            {
                var server = ConnectionWrapper.Server(ep);
                var keys = server.Keys(pattern: "*" + pattern + "*");
                foreach (var key in keys)
                    Db.KeyDelete(key);
            }
        }

        public void Set(string key, object data)
        {
            Set(key, data, InternalCacheConst.DefaultExpirate);
        }

        public void Set(string key, object data, int expirate)
        {
            if (data == null) throw new ArgumentException("缓存值data不能为空或null", "data");

            byte[] entryBytes = Serialize(data);
            TimeSpan expiresIn = TimeSpan.FromSeconds(expirate);

            Db.StringSet(key, entryBytes, expiresIn);
        } 
        #endregion
    }
}
