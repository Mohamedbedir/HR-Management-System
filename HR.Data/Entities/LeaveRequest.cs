using HR.Data.Entities.Common;
using HR.Data.Enums;

namespace HR.Data.Entities
{
    public class LeaveRequest: BaseEntity
    {
        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }

        public string? Reason { get; set; }

        public LeaveRequestStatus Status { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public int EmployeeId { get; set; }

        public Employee Employee { get; set; } = null!;
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; } = null!;
        public int? ApprovedById { get; set; }
        public Employee? ApprovedBy { get; set; }
    }
}