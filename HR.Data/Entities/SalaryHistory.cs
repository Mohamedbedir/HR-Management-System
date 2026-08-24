using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Entities
{
    public class SalaryHistory
    {
        public long Id { get; set; }


        public decimal Salary { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Reason { get; set; }

        // Navigation
        public int? EmployeeId { get; set; }

        public Employee? Employee { get; set; } 
    }
}
