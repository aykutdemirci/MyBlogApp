using Microsoft.Extensions.DependencyInjection;
using MyBlogApp.Application.Abstractions.Caching;
using MyBlogApp.Application.Abstractions.Storage;
using MyBlogApp.Infrastructure.Configurations;
using MyBlogApp.Infrastructure.Enums;
using MyBlogApp.Infrastructure.Services.Caching;

namespace MyBlogApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {

        }

        public static void AddCache(this IServiceCollection serviceCollection, string environmentName, CachingType cachingType)
        {
            switch (cachingType)
            {
                case CachingType.InMemory:
                    serviceCollection.AddMemoryCache();
                    serviceCollection.AddScoped<ICacheService, InMemoryCacheService>();
                    break;
                case CachingType.Distributed:
                    serviceCollection.AddStackExchangeRedisCache(opts =>
                    {
                        opts.Configuration = RedisConfig.GetConnectionString(environmentName);
                    });
                    serviceCollection.AddScoped<ICacheService, DistributedCacheService>();
                    break;
            }
        }

        public static void AddStorageService<T>(this IServiceCollection serviceCollection) where T : class, IStorageService
        {
            serviceCollection.AddScoped<IStorageService, T>();
        }
    }
}
