using HR.Core.Features.JobPostings.Queries.Responses;
using HR.Data.Entities.Recruitment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.JobPostings
{
    public partial class JobPostingProfile
    {
        public void GetJobPostingsMapping()
        {
            CreateMap<JobPosting, GetJobPostingsResponse>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department.Name))
                .ForMember(d => d.PositionName, o => o.MapFrom(s => s.Position.Title));
                
        }
    }
}
