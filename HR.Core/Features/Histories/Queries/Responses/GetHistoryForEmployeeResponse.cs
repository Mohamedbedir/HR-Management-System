using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Histories.Queries.Responses
{
    public class GetHistoryForEmployeeResponse
    {
        public long Id { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public string? Reason { get; set; }
        public int EmployeeId { get; set; }

        public int DepartmentId { get; set; }

        public int PositionId { get; set; }

        

        // Navigation
        public string EmployeeName { get; set; } 

        public string DepartmentName { get; set; } 

        public string PositionTitle { get; set; } 
    }
}
