using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Applications.Queries.Models;
using HR.Core.Features.Applications.Queries.Responses;
using HR.Core.Features.Candidates.Queries.Models;
using HR.Core.Features.Candidates.Queries.Responses;
using HR.Core.Features.Departments.Queries.Models;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Core.Features.JobPostings.Queries.Models;
using HR.Core.Features.JobPostings.Queries.Responses;
using HR.Service.Services;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Candidates.Queries.Handlers
{
    public class CandidateQueryHandler : ResponseHandler,
        IRequestHandler<GetCandidateByIdQuery, Response<GetCandidateByIdResponse>>,
        IRequestHandler<GetCandidatesQuery, Response<IReadOnlyList<GetCandidatesResponse>>>
    {
        private readonly ICandidateService candidateService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> localizer;

        public CandidateQueryHandler(ICandidateService candidateService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer):base(localizer)
        {
            this.candidateService = candidateService;
            this.mapper = mapper;
            this.localizer = localizer;
        }
       
        public async Task<Response<GetCandidateByIdResponse>> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            var candidate = await candidateService.GetByIdAsync(request.Id);
            if (candidate == null)
                return NotFound<GetCandidateByIdResponse>();
            var candidate_Mapped = mapper.Map<GetCandidateByIdResponse>(candidate);
            return Success(candidate_Mapped);
        }

        public async Task<Response<IReadOnlyList<GetCandidatesResponse>>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
        {
            var candidates = await candidateService.GetAllAsync();
            var candidates_Mapped = mapper.Map<IReadOnlyList<GetCandidatesResponse>>(candidates);
            return Success(candidates_Mapped, Meta: new { DataCount = candidates_Mapped.Count() });
        }
    }
}
