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
    public class EmploymentHistoryRepo : GenericRepos<EmploymentHistory>, IEmploymentHistoryRepo
    {
        public EmploymentHistoryRepo(HRAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
