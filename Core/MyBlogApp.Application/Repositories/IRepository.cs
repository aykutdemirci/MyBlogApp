using Microsoft.EntityFrameworkCore;
using MyBlogApp.Domain.Entities.Common;
using System.Linq.Expressions;

namespace MyBlogApp.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntitiy
    {
        DbSet<T> Table { get; }

        Task<bool> AddAsync(T entity);

        Task<bool> AddRangeAsync(List<T> entities);

        bool Delete(T entity);

        bool DeleteRange(List<T> entities);

        bool Update(T entity);

        IQueryable<T> GetAll(bool tracking = true);

        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = true);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = true);

        Task<T> GetByIdAsync(string id, bool tracking = true);
    }
}
