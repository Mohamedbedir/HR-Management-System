using HR.Core.Features.Employees.Commands.Models;
using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Employees
{
    public partial class EmployeeProfile
    {
        public void AddEmployeeMapping()
        {
            CreateMap<AddEmployeeCommand, Employee>();
        }
    }
}
