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
                cfgManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MyBlogApp.API"));
                cfgManager.AddJsonFile("appsettings.json");
                return cfgManager.GetConnectionString("Redis");
            }
        }

        public static string GetConnectionString(string environmentName)
        {
            var cfgManager = new ConfigurationManager();
            cfgManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MyBlogApp.API"));
            cfgManager.AddJsonFile($"appsettings.{environmentName}.json");
            return cfgManager.GetConnectionString("Redis");
        }
    }
}
