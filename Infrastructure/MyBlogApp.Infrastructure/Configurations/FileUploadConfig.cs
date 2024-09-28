using Microsoft.Extensions.Configuration;
using MyBlogApp.Infrastructure.Services.Storage;

namespace MyBlogApp.Infrastructure.Configurations
{
    public static class FileUploadConfig
    {
        public static string GetAuthorImagesPath(string environmentName, Type storageServiceType)
        {
            var cfgManager = new ConfigurationManager();

            cfgManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MyBlogApp.API"));
            cfgManager.AddJsonFile($"appsettings.{environmentName}.json");

            var authorsImagePath = cfgManager["Storages:Local:FilePaths:AuthorImagesPath"];

            if (storageServiceType == typeof(AzureStorageService))
            {
                authorsImagePath = cfgManager["Storages:Azure:FilePaths:AuthorImagesPath"];
            }

            return authorsImagePath;
        }

        public static string GetAzureConnectionString(string environmentName)
        {
            var cfgManager = new ConfigurationManager();

            cfgManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/MyBlogApp.API"));
            cfgManager.AddJsonFile($"appsettings.{environmentName}.json");

            return cfgManager["Storages:Azure:ConnectionString"];
        }
    }
}
