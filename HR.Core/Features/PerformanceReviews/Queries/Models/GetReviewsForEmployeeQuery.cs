using HR.Core.Bases;
using HR.Core.Features.PerformanceReviews.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.PerformanceReviews.Queries.Models
{
    public class GetReviewsForEmployeeQuery:IRequest<Response<List<GetReviewsForEmployeeResponse>>>
    {
        public int EmployeeId { get; set; }

        public GetReviewsForEmployeeQuery(int employeeId)
        {
            EmployeeId = employeeId;
        }
    }
}
