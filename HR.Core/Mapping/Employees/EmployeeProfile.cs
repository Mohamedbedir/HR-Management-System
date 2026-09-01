using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Employees
{
    public partial class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            GetEmployeeByIdMapping();
            GetEmployeesMapping();

            AddEmployeeMapping();
        }
    }
}
