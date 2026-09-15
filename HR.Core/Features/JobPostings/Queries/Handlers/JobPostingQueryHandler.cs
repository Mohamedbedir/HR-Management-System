using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Departments.Queries.Models;
using HR.Core.Features.Departments.Queries.Responses;
using HR.Core.Features.JobPostings.Queries.Models;
using HR.Core.Features.JobPostings.Queries.Responses;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.JobPostings.Queries.Handlers
{
    public class JobPostingQueryHandler: ResponseHandler,
        IRequestHandler<GetJobPostingByIdQuery,Response<GetJobPostingByIdResponse>>,
        IRequestHandler<GetJobPostingsQuery, Response<IReadOnlyList<GetJobPostingsResponse>>>
    {
        private readonly IJobPostingService jobPostingService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> localizer;

        public JobPostingQueryHandler(IJobPostingService jobPostingService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer):base(localizer)
        {
            this.jobPostingService = jobPostingService;
            this.mapper = mapper;
            this.localizer = localizer;
        }
        public async Task<Response<GetJobPostingByIdResponse>> Handle(GetJobPostingByIdQuery request, CancellationToken cancellationToken)
        {
            var job = await jobPostingService.GetJobPostingByIdAsync(request.Id);
            if (job == null)
                return NotFound<GetJobPostingByIdResponse>();
            var job_Mapped = mapper.Map<GetJobPostingByIdResponse>(job);
            return Success(job_Mapped);
        }
        

        public async Task<Response<IReadOnlyList<GetJobPostingsResponse>>> Handle(GetJobPostingsQuery request, CancellationToken cancellationToken)
        {
            var jobs = await jobPostingService.GetAllJobPostingsAsync();
            var jobs_Mapped = mapper.Map<IReadOnlyList<GetJobPostingsResponse>>(jobs);
            return Success(jobs_Mapped, Meta: new { DataCount = jobs_Mapped.Count() });
        }
    }
}
