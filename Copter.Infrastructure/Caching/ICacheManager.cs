using System;

namespace Copter.Infrastructure.Caching
{
    /// <summary>
    /// 缓存管理接口 - 默认缓存两小时
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">类型 T</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>T 对象</returns>
        T Get<T>(string key);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">类型 T</typeparam>
        /// <param name="key">缓存键</param>
        /// <param name="func">获取缓存前 所要执行的 委托</param>
        /// <returns>T 对象</returns>
        T Get<T>(string key, Action func);

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">数据对象</param>
        void Set(string key, object data);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">数据对象</param>
        /// <param name="expirate">失效时间 单位秒</param>
        void Set(string key, object data, int expirate);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        bool Contains(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        void Remove(string key);

        /// <summary>
        /// 批量移除
        /// </summary>
        /// <param name="pattern">缓存键格式 - 正则表达式</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        void Clear();
    }
}
