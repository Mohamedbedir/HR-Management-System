using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Repositories.Contract
{
    public interface IDepartmentRepo : IGenericRepos<Department>
    {
        Task<IReadOnlyList<Department>> GetDepartmentsAsync();
    }
}
