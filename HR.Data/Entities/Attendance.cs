using HR.Data.Entities.Common;
using HR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Entities
{
    public class Attendance:BaseEntity
    {

        public int EmployeeId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly? CheckIn { get; set; }

        public TimeOnly? CheckOut { get; set; }

        public AttendanceStatus Status { get; set; }

        public int LateMinutes { get; set; }

        public int OvertimeMinutes { get; set; }

        public string? Notes { get; set; }

        // Navigation
        public Employee Employee { get; set; } = null!;
    }
}
