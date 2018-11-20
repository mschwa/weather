using System;
using Microsoft.Extensions.Caching.Memory;

namespace Weather.Api.Services
{
    public interface ICachingService
    {
        void Set(string cacheKey, object data, DateTimeOffset offset);
        bool TryGetValue<T>(string cacheKey, out T cached);
    }

    public class CachingService : ICachingService
    {
        private readonly IMemoryCache _memoryCache;

        public CachingService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Set(string cacheKey, object data, DateTimeOffset offset)
        {
            _memoryCache.Set($"{cacheKey}", data, offset);
        }

        public bool TryGetValue<T>(string cacheKey, out T value)
        {
            return _memoryCache.TryGetValue(cacheKey, out value);
        }
    }
}
