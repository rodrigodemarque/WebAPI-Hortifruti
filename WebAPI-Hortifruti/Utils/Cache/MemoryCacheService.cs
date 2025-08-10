using System;
using System.Runtime.Caching;

namespace WebAPI_Hortifruti.Utils.Cache
{
    public class MemoryCacheService : ICacheService
    {
        private readonly ObjectCache cache;

        public MemoryCacheService()
        {
            cache = MemoryCache.Default;
        }
        public T Get<T>(string key)
        {
            return (T)cache.Get(key);
        }

        public void Set<T>(string key, T value, int cacheSeconds)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(cacheSeconds);
            cache.Set(key, value, policy);
            //cache.Set(key, value, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(cacheSeconds) });
        }

        public void Remove(string key)
        {
            cache.Remove(key);
        }
    }
}