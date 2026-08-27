using HR.Data.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Repositories.Contract
{
    public interface IGenericRepos<T> where T : class
    {
        // Queries
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        IQueryable<T> GetTableNoTracking();

        IQueryable<T> GetTableAsTracking();

        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

        // Add
        Task<T> AddAsync(T entity);

        Task AddRangeAsync(ICollection<T> entities);

        // Update
        void UpdateAsync(T entity);

        void UpdateRangeAsync(
            ICollection<T> entities);

        // Delete
        void DeleteAsync(T entity);

        void DeleteRangeAsync(ICollection<T> entities);

        // Unit of Work operations
        Task SaveChangesAsync();

        // Transactions
        Task<IDbContextTransaction> BeginTransactionAsync();

        Task CommitAsync();

        Task RollBackAsync();
    }
}
