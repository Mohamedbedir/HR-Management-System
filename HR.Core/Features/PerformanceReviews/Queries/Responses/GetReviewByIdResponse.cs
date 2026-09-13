using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.PerformanceReviews.Queries.Responses
{
    public class GetReviewByIdResponse
    {
        public int Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime ReviewDate { get; set; }

        public decimal Score { get; set; }

        public string? Comments { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; } = null!;
    }
}
