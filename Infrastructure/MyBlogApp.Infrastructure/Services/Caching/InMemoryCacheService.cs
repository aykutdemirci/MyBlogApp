using Microsoft.Extensions.Caching.Memory;
using MyBlogApp.Application.Abstractions.Caching;

namespace MyBlogApp.Infrastructure.Services.Caching
{
    public class InMemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public InMemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Add(string key, object value)
        {
            _memoryCache.Set(key, value);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public bool TryGetValue<T>(string key, out T? value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }
    }
}
