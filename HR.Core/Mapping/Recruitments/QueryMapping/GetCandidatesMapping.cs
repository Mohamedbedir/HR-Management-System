using HR.Core.Features.Candidates.Queries.Responses;
using HR.Core.ResolverFile;
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
        public void GetCandidatesMapping()
        {
            CreateMap<Candidate, GetCandidatesResponse>()
                .ForMember(d => d.CV, o => o.MapFrom<CandidateCVFileResolver>());
        }
    }
}
