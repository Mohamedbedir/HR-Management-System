using HR.Core.Features.Departments.Queries.Responses;
using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Departments
{
    public partial class DepartmentProfile
    {
        public void GetDepartmentsMapping()
        {
            CreateMap<Department, GetDepartmentsResponse>();

        }
    }
}
