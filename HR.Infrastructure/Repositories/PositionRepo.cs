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
    public class PositionRepo : GenericRepos<Position>, IPositionRepo
    {
        public PositionRepo(HRAppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
