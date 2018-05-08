namespace Copter.Infrastructure.Caching
{
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheType
    {
        MemoryCache,
        RuntimeCache,
        RedisCache,
        MemcachedCache,
        PerRequestCache
    }
}
