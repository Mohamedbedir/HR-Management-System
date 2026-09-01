using HR.Data.Entities;
using HR.Infrastructure.Contexts;
using HR.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Repositories
{
    public class EmployeeRepo : GenericRepos<Employee>, IEmployeeRepo
    {
        private readonly HRAppDbContext dbContext;

        public EmployeeRepo(HRAppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public override async Task<IReadOnlyList<Employee>> GetAllAsync()
        {
            return await dbContext.Employees.
                Include(d => d.Department)
               .Include(p => p.Position)
               .Include(m => m.Manager)
               .ToListAsync();
        }
    }
}
