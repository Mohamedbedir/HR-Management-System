using HR.Data.Entities.Common;
using HR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Entities
{
    public class Payroll: BaseEntity
    {

        public int EmployeeId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal GrossSalary { get; set; }

        public decimal TotalDeductions { get; set; }

        public decimal NetSalary { get; set; }

        public PayrollStatus Status { get; set; }

        public DateTime GeneratedAt { get; set; }

        // Navigation
        public Employee Employee { get; set; } = null!;

        public ICollection<PayrollItem> Items { get; set; }
            = new HashSet<PayrollItem>();
    }
}
