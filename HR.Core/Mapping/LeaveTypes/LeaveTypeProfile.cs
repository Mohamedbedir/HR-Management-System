using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.LeaveTypes
{
    public partial class LeaveTypeProfile:Profile
    {
        public LeaveTypeProfile() 
        {
            GetLeaveTypeByIdMapping();
            GetLeaveTypesMapping();

            AddLeaveTypeMapping();
        }
    }
}
