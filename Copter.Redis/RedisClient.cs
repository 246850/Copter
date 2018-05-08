using System;
using System.Text.RegularExpressions;
using StackExchange.Redis;

namespace Copter.Redis
{
    /// <summary>
    /// Redis 客户端操作类型 应确保IConnectionMultiplexer只有一个实例 - 集群
    /// </summary>
    public partial class RedisClient
    {
        /// <summary>
        /// 键前缀 默认为：default，默认失效时间2小时 - 构造函数
        /// </summary>
        /// <param name="connectionWrapper">redis连接池</param>
        public RedisClient(IRedisConnectionWrapper connectionWrapper):this(connectionWrapper, "default", TimeSpan.FromHours(2))
        {
        }

        /// <summary>
        /// 自定义键前缀 - 构造函数
        /// </summary>
        /// <param name="connectionWrapper">redis连接池</param>
        /// <param name="keyPrefix">键 前缀</param>
        /// <param name="expiry">失效时间</param>
        public RedisClient(IRedisConnectionWrapper connectionWrapper, string keyPrefix, TimeSpan expiry)
        {
            if(string.IsNullOrWhiteSpace(keyPrefix) || !Regex.IsMatch(@"^[a-zA-Z]\w+$", keyPrefix)) throw new Exception(@"redis键前缀正确格式必须符合：^[a-zA-Z]\w+$");

            ConnectionWrapper = connectionWrapper;
            Db = ConnectionWrapper.GetDatabase();
            KeyPrefix = keyPrefix;
            Expiration = expiry;
            RealKeySuffix = "d.r.s"; //data.real.suffix
        }
        /// <summary>
        /// Redis 连接池
        /// </summary>
        protected IRedisConnectionWrapper ConnectionWrapper { get; }
        /// <summary>
        /// Redis 连接数据库
        /// </summary>
        protected IDatabase Db { get; }
        /// <summary>
        /// 键前缀
        /// </summary>
        protected string KeyPrefix { get; }
        /// <summary>
        /// 失效时间
        /// </summary>
        protected TimeSpan Expiration { get; }
        /// <summary>
        /// 真实缓存数据键后缀 - 用于二级缓存，避免缓存雪崩
        /// </summary>
        protected string RealKeySuffix { get; set; }

        #region - 公共方法
        /// <summary>
        /// 构建redis key - 带前缀
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RedisKey BuildKey(string key)
        {
            return string.Concat(KeyPrefix, ".", key);
        }
        #endregion
    }
}
