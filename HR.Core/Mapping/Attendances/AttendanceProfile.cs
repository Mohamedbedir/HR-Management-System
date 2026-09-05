using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Attendances
{
    public partial class AttendanceProfile:Profile
    {
        public AttendanceProfile()
        {
            GetAttendancesForEmpliyeeMapping();
            GetAttendancesForEmpliyeeByDateMapping();
        }
    }
}
