using HR.Core.Features.Attendances.Queries.Responses;
using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Attendances
{
    public partial class AttendanceProfile
    {
        public void GetAttendancesForEmpliyeeMapping()
        {
            CreateMap<Attendance, GetAttendancesForEmpliyeeResponse>()
                .ForMember(d=>d.EmployeeName,o=>o.MapFrom(s=>s.Employee.FirstName + " " + s.Employee.LastName))
                .ForMember(d=>d.Status,o=>o.MapFrom(s=>s.Status.ToString()));
        }
    }
}
