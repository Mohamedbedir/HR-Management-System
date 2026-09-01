using HR.Core.Features.Employees.Queries.Responses;
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
        public void GetEmployeesMapping()
        {
            CreateMap<Employee, GetEmployeesResponse>()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department.Name))
                .ForMember(d => d.PositionTitle, o => o.MapFrom(s => s.Position.Title))
                .ForMember(d => d.ManagerName, o => o.MapFrom(s => s.Manager.FirstName + " " + s.Manager.LastName));

            
        }
    }
}
