namespace MyBlogApp.Application.Abstractions.Caching
{
    public interface ICacheService
    {
        void Add(string key, object value);

        void Remove(string key);

        bool TryGetValue<T>(string key, out T? value);
    }
}
