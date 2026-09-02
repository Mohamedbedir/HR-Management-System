using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Histories.Queries.Responses
{
    public class GetSalaryHistoryForEmployeeResponse
    {
        public long Id { get; set; }

        public decimal Salary { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? Reason { get; set; }

        // Navigation
        public int? EmployeeId { get; set; }

        public string? EmployeeName { get; set; }
    }
}
