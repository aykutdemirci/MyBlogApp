using Microsoft.Extensions.Configuration;

namespace MyBlogApp.Persistance
{
    public class Configuration
    {
        public static string DbConnectionString 
        { 
            get 
            {
                var configurationManager = new ConfigurationManager();

                var path = Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MyBlogApp.UI");

                configurationManager.SetBasePath(path);

                configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("MyBlogAppDbConnectionString");
            }
        }

        public static string GetConnectionString(string environmentName)
        {
            var configurationManager = new ConfigurationManager();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MyBlogApp.UI");

            configurationManager.SetBasePath(path);

            configurationManager.AddJsonFile($"appsettings.{environmentName}.json");

            return configurationManager.GetConnectionString("MyBlogAppDbConnectionString");
        }
    }
}
