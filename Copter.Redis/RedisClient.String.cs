using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Copter.Redis
{
	/// <summary>
    /// String类型操作
    /// </summary>
    public partial class RedisClient
    {
	    /// <summary>
	    /// 原子性自减 - key存在，自减；反之，先设置0，再自减
	    /// </summary>
	    /// <param name="key">键</param>
	    /// <param name="decrement">自减增量 默认1</param>
	    /// <returns>自减后的值</returns>
	    public long StringDecrement(string key, long decrement = 1)
	    {
	        return Db.StringDecrement(BuildKey(key));
	    }

        /// <summary>
        /// 原子性自增 - key存在，自减；反之，先设置0，再自增
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="increment">自增增量 默认1</param>
        /// <returns>自增后的值</returns>
        public long StringIncrement(string key, long increment = 1)
        {
            return Db.StringIncrement(BuildKey(key));
        }

        /// <summary>
        /// 根据key 获取字符串 - 当 key 不存在时，返回 null ，否则，返回 key 的值。
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>值</returns>
        public string StringGet(string key)
	    {
            return Db.StringGet(BuildKey(key));
	    }

	    /// <summary>
	    /// 设置缓存，key存在则覆盖，不存在则添加 - 使用默认失效时间
	    /// </summary>
	    /// <param name="key">键</param>
	    /// <param name="value">值</param>
	    public bool StringSet(string key, string value)
	    {
            return StringSet(BuildKey(key), value, Expiration);
	    }

        /// <summary>
        /// 设置缓存，key存在则覆盖，不存在则添加
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">失效时间</param>
        public bool StringSet(string key, string value, TimeSpan expiry)
        {
            return Db.StringSet(BuildKey(key), value, expiry);
        }
    }
}
