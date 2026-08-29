using HR.Data.Entities;
using HR.Infrastructure.Contexts;
using HR.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Repositories
{
    public class LeaveTypeRepo : GenericRepos<LeaveType>, ILeaveTypeRepo
    {
        private readonly HRAppDbContext dbContext;

        public LeaveTypeRepo(HRAppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
