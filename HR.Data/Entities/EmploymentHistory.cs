using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Entities
{
    public class EmploymentHistory
    {
        public long Id { get; set; }

        public int EmployeeId { get; set; }

        public int DepartmentId { get; set; }

        public int PositionId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Reason { get; set; }

        // Navigation
        public Employee Employee { get; set; } = null!;

        public Department Department { get; set; } = null!;

        public Position Position { get; set; } = null!;
    }
}
