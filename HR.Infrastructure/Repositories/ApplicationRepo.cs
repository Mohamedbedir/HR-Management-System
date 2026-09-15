using HR.Data.Entities.Recruitment;
using HR.Infrastructure.Contexts;
using HR.Infrastructure.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Repositories
{
    public class ApplicationRepo : GenericRepos<Application>, IApplicationRepo
    {
        public ApplicationRepo(HRAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
