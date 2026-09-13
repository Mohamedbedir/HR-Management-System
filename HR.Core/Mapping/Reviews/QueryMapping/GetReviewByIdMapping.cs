using HR.Core.Features.PerformanceReviews.Queries.Responses;
using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Reviews
{
    public partial class ReviewProfile
    {
        public void GetReviewByIdMapping()
        {
            CreateMap<PerformanceReview, GetReviewByIdResponse>()
                .ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
                .ForMember(d => d.ReviewerName, o => o.MapFrom(s => s.Reviewer.FirstName + " " + s.Reviewer.LastName));
            CreateMap<PerformanceReview, GetReviewsResponse>()
                .ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
                .ForMember(d => d.ReviewerName, o => o.MapFrom(s => s.Reviewer.FirstName + " " + s.Reviewer.LastName));
            CreateMap<PerformanceReview, GetReviewsForEmployeeResponse>()
                .ForMember(d => d.EmployeeName, o => o.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
                .ForMember(d => d.ReviewerName, o => o.MapFrom(s => s.Reviewer.FirstName + " " + s.Reviewer.LastName));
        }
    }
}
