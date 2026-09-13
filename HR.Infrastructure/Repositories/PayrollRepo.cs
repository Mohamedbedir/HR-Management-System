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
    public class PayrollRepo : GenericRepos<Payroll>, IPayrollRepo
    {
        public PayrollRepo(HRAppDbContext context)
            : base(context)
        {
        }
    }
}
