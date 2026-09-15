using HR.Core.Bases;
using HR.Core.Features.Applications.Queries.Responses;
using HR.Core.Features.Candidates.Queries.Responses;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Core.Features.JobPostings.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Candidates.Queries.Models
{
    public class GetCandidatesQuery : IRequest<Response<IReadOnlyList<GetCandidatesResponse>>>
    {
    }
}
