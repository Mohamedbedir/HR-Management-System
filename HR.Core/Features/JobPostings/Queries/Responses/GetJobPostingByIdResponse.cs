using HR.Data.Entities;
using HR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.JobPostings.Queries.Responses
{
    public class GetJobPostingByIdResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public JobPostingStatus Status { get; set; }

        public DateTime PostedAt { get; set; }

        public DateTime? ClosingDate { get; set; }

        // Navigation
        public string? DepartmentName { get; set; }
        public string? PositionName { get; set; }

        
    }
}
