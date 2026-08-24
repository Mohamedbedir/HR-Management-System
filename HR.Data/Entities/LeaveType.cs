using HR.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Entities
{
    public class LeaveType:BaseEntity
    {

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int MaxDaysPerYear { get; set; }

        public bool IsPaid { get; set; }

        public bool IsActive { get; set; }

        public ICollection<LeaveRequest> LeaveRequests { get; set; }
            = new List<LeaveRequest>();
    }
}
