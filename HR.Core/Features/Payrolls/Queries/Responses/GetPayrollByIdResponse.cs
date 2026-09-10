using HR.Data.Entities;
using HR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Payrolls.Queries.Responses
{
    public class GetPayrollByIdResponse
    {
        public int Id { get; set; }
        public int Month { get; set; }

        public int Year { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal GrossSalary { get; set; }

        public decimal TotalDeductions { get; set; }

        public decimal NetSalary { get; set; }

        public PayrollStatus Status { get; set; }

        public DateTime GeneratedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }

        public DateTime? PaidAt { get; set; }

        // Navigation
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } 

        public List<PayrollItemResponse> Items { get; set; }
        
    }

    public class PayrollItemResponse
    {
        public string Name { get; set; } 

        public PayrollItemType Type { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }
    }

}
