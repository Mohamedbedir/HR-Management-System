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
    public class DepartmentRepo: GenericRepos<Department>,IDepartmentRepo
    {
        private readonly HRAppDbContext dbContext;

        public DepartmentRepo(HRAppDbContext dbContext):base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IReadOnlyList<Department>> GetDepartmentsAsync()
        {
            return await dbContext.Departments.ToListAsync();
        }
    }
}
