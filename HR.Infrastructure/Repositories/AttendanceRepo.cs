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
    public class AttendanceRepo: GenericRepos<Attendance>,IAttendanceRepo
    {
        public AttendanceRepo(HRAppDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
