using HR.Core.Features.Histories.Queries.Responses;
using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Histories
{
    public partial class HistoryProfile
    {
        public void GetSalaryHistoryForEmployeeMapping()
        {
            CreateMap<SalaryHistory, GetSalaryHistoryForEmployeeResponse>()
                .ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName));
        }
    }
}
