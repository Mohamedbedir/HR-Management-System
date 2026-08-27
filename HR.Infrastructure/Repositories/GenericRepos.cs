using HR.Infrastructure.Contexts;
using HR.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Repositories
{
    public class GenericRepos<T> : IGenericRepos<T> where T : class
    {
        private readonly HRAppDbContext dbContext;

        public GenericRepos(HRAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            await dbContext.Set<T>().AddRangeAsync(entities);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) // x => x.id == 5  or x => x.IsActive
        {
            return await dbContext.Set<T>().AnyAsync(predicate);  // true or false  
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
           return await dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await dbContext.Database.CommitTransactionAsync();
        }

        public virtual void DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public virtual void DeleteRangeAsync(ICollection<T> entities)
        {
            dbContext.Set<T>().RemoveRange(entities);
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await dbContext.Set<T>().FindAsync(id);
        }

        public virtual IQueryable<T> GetTableAsTracking()
        {
            return dbContext.Set<T>().AsTracking().AsQueryable();
        }

        public virtual IQueryable<T> GetTableNoTracking()
        {
            return dbContext.Set<T>().AsNoTracking();
        }

        public async Task RollBackAsync()
        {
            await dbContext.Database.RollbackTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public virtual void UpdateAsync(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

        public virtual void UpdateRangeAsync(ICollection<T> entities)
        {
            dbContext.Set<T>().UpdateRange(entities);
        }
    }
}
