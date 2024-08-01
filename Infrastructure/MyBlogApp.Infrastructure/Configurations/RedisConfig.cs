using Microsoft.Extensions.Configuration;

namespace MyBlogApp.Infrastructure.Configurations
{
    public static class RedisConfig
    {
        public static string ConnectionString
        {
            get
            {
                var cfgManager = new ConfigurationManager();
                cfgManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MyBlogApp.UI"));
                cfgManager.AddJsonFile("appsettings.json");
                return cfgManager.GetConnectionString("Redis");
            }
        }
    }
}
