using HR.Core.Features.Payrolls.Queries.Responses;
using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Payrolls
{
    public partial class PayrollProfile
    {
        public void GetPayrollsForEmployeeMapping()
        {
            CreateMap<Payroll, GetPayrollsForEmployeeResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.EmployeeName,
                opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName));
        }
    }
}
