using HR.Core.Features.LeaveTypes.Queries.Responses;
using HR.Core.Features.Positions.Queries.Responses;
using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.LeaveTypes
{
    public partial class LeaveTypeProfile
    {
        public void GetLeaveTypesMapping()
        {
            CreateMap<LeaveType, GetLeaveTypesResponse>();
        }
    }
}
