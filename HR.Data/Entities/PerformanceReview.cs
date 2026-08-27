using HR.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Entities
{
    public class PerformanceReview:BaseEntity
    {
        public DateTime ReviewDate { get; set; }

        public decimal Score { get; set; }

        public string? Comments { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
        public int ReviewerId { get; set; }
        public Employee Reviewer { get; set; } = null!;
    }
}
