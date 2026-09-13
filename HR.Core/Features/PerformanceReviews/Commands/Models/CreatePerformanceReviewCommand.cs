using HR.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.PerformanceReviews.Commands.Models
{
    public class CreatePerformanceReviewCommand : IRequest<Response<string>>
    {
        public int EmployeeId { get; set; }

        public int ReviewerId { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public decimal Score { get; set; }

        public string? Comments { get; set; }
    }
}
