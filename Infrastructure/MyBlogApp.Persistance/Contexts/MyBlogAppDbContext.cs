using Microsoft.EntityFrameworkCore;
using MyBlogApp.Domain.Entities;
using MyBlogApp.Domain.Entities.Common;

namespace MyBlogApp.Persistance.Contexts
{
    public class MyBlogAppDbContext : DbContext
    {
        public MyBlogAppDbContext(DbContextOptions options) : base(options)
        {

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntitiy>();

            foreach (var item in entries)
            {
                if (item.State == EntityState.Added)
                {
                    item.Entity.CreateDate = DateTime.Now;
                }
                else if (item.State == EntityState.Modified)
                {
                    item.Entity.UpdateDate = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
