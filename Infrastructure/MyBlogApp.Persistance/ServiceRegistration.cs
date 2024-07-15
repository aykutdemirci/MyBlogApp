using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyBlogApp.Persistance.Contexts;

namespace MyBlogApp.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<MyBlogAppDbContext>(opts => opts.UseSqlServer(Configuration.DbConnectionString));
        }
    }
}
