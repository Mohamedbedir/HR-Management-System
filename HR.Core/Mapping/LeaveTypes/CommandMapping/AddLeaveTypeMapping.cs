using HR.Core.Features.LeaveTypes.Commands.Models;
using HR.Core.Features.Positions.Commands.Models;
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
        public void AddLeaveTypeMapping()
        {
            CreateMap<AddLeaveTypeCommand, LeaveType>();
        }
    }
}
