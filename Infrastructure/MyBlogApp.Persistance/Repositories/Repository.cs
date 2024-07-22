using Microsoft.EntityFrameworkCore;
using MyBlogApp.Application.Repositories;
using MyBlogApp.Domain.Entities.Common;
using MyBlogApp.Persistance.Contexts;
using System.Linq.Expressions;

namespace MyBlogApp.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntitiy
    {
        private readonly MyBlogAppDbContext _dbContext;

        public Repository(MyBlogAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DbSet<T> Table => _dbContext.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            var entityEntry = await Table.AddAsync(entity);
            return entityEntry.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }

        public bool Update(T entity)
        {
            var entityEntry = Table.Update(entity);
            return entityEntry.State == EntityState.Modified;
        }

        public bool Delete(T entity)
        {
            var entityEntry = Table.Remove(entity);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool DeleteRange(List<T> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking) query = query.AsNoTracking();

            return query;
        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking) query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(q => q.Id == Guid.Parse(id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = true)
        {
            var query = Table.AsQueryable();

            if (!tracking) query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true)
        {
            var query = Table.Where(predicate);

            if (!tracking) query = query.AsNoTracking();

            return query;
        }
    }
}
