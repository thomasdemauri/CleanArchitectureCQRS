using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CleanArchitecture.Infrastructure.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        public CachingService(IDistributedCache cache)
        {
            _cache = cache;

            _options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200),
            };
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var data = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(data))
                return default;

            return JsonSerializer.Deserialize<T>(data);
        }

        public async Task SetAsync<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, json, _options);
        }
    }
}
