using HR.Data.Entities;
using HR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveRequests.Queries.Responses
{
    public class GetLeaveRequestByIdRespose
    {
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Reason { get; set; }

        public LeaveRequestStatus Status { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? RejectionReason { get; set; }
        // Navigation
        public string EmployeeName { get; set; }
        public string LeaveType { get; set; } 
        public string ApprovedByName { get; set; }
    }
}
