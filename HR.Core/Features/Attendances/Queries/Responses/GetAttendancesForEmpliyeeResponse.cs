using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Attendances.Queries.Responses
{
    public class GetAttendancesForEmpliyeeResponse
    {
        public int EmployeeId { get; set; }

        public DateOnly Date { get; set; }

        public TimeOnly? CheckIn { get; set; }

        public TimeOnly? CheckOut { get; set; }

        public string Status { get; set; }

        public int LateMinutes { get; set; }

        public int OvertimeMinutes { get; set; }

        public string? Notes { get; set; }

        // Navigation
        public string EmployeeName { get; set; }
    }
}
