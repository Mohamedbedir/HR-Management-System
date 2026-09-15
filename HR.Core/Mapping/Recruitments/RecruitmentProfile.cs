using AutoMapper;
using HR.Core.Features.Applications.Queries.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Mapping.Recruitments
{
    public partial class RecruitmentProfile:Profile
    {
        public RecruitmentProfile()
        {
            GetCandidatesMapping();
            GetCandidateByIdMapping();

            GetApplicationsMapping();
            GetApplicationByIdMapping();
        }
    }
}
