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
    public class GetReviewByIdQuery:IRequest<Response<GetReviewByIdResponse>>
    {
        public int Id { get; set; }

        public GetReviewByIdQuery(int id)
        {
            Id = id;
        }
    }
}
