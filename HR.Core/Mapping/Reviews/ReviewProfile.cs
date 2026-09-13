using AutoMapper;
using HR.Core.Features.PerformanceReviews.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Reviews
{
    public partial class ReviewProfile:Profile
    {
        public ReviewProfile()
        {
            GetReviewByIdMapping();
        }
    }
}
