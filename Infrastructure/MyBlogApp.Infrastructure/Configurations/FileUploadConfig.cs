using Microsoft.Extensions.Configuration;

namespace MyBlogApp.Infrastructure.Configurations
{
    public static class FileUploadConfig
    {
        public static string GetBasePath(string environmentName)
        {
            var cfgManager = new ConfigurationManager();

            cfgManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MyBlogApp.UI"));
            cfgManager.AddJsonFile($"appsettings.{environmentName}.json");

            return cfgManager["FilePaths:BasePath"];
        }

        public static string GetAuthorImagesPath(string environmentName)
        {
            var cfgManager = new ConfigurationManager();

            cfgManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MyBlogApp.UI"));
            cfgManager.AddJsonFile($"appsettings.{environmentName}.json");

            var authorImagesPath = cfgManager["FilePaths:AuthorImagesPath"];
            authorImagesPath = GetBasePath(environmentName) + authorImagesPath;

            return authorImagesPath;
        }
    }
}
