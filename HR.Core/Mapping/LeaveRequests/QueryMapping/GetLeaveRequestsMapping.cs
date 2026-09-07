using HR.Core.Features.LeaveRequests.Queries.Responses;
using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.LeaveRequests
{
    public partial class LeaveRequestProfile
    {
        public void GetLeaveRequestsMapping()
        {
            CreateMap<LeaveRequest, GetLeaveRequestsRespose>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
                .ForMember(d => d.LeaveType, o => o.MapFrom(s => s.LeaveType.Name))
                .ForMember(d => d.ApprovedByName, o => o.MapFrom(s => s.ApprovedBy.FirstName + " " + s.ApprovedBy.LastName))
                ;
        }
    }
}