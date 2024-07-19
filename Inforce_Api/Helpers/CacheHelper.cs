using Microsoft.Extensions.Caching.Memory;

namespace Inforce_Api.Helpers
{
    public class CacheHelper
    {
        private readonly IMemoryCache _cache;
        public CacheHelper(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public T AddCache<T>(T cache, string key)
        {
            var options = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromSeconds(5),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(15)
            };

            _cache.Set<T>(key, cache, options);

            return cache;
        }
    }
}
