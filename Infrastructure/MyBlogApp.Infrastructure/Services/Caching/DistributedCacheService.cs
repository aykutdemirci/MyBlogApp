using MyBlogApp.Application.Abstractions.Caching;

namespace MyBlogApp.Infrastructure.Services.Caching
{
    public class DistributedCacheService : ICacheService
    {
        public void Add(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue<T>(string key, out T? value)
        {
            throw new NotImplementedException();
        }
    }
}
