using HR.Core.Features.Applications.Queries.Responses;
using HR.Data.Entities.Recruitment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Recruitments
{
    public partial class RecruitmentProfile 
    {
        public void GetApplicationsMapping()
        {
            CreateMap<Application, GetApplicationsResponse>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.CandidateName, o => o.MapFrom(s => s.Candidate.FirstName + " " + s.Candidate.LastName))
                .ForMember(d => d.JobPostTitle, o => o.MapFrom(s => s.JobPosting.Title));
        }
    }
}
