using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Applications.Queries.Models;
using HR.Core.Features.Applications.Queries.Responses;
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

namespace HR.Core.Features.Applications.Queries.Handlers
{
    public class ApplicationQueryHandler : ResponseHandler,
        IRequestHandler<GetApplicationByIdQuery, Response<GetApplicationByIdResponse>>,
        IRequestHandler<GetApplicationsQuery, Response<IReadOnlyList<GetApplicationsResponse>>>
    {
        private readonly IApplicationService applicationService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> localizer;

        public ApplicationQueryHandler(IApplicationService applicationService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer):base(localizer)
        {
            this.applicationService = applicationService;
            this.mapper = mapper;
            this.localizer = localizer;
        }
       
        public async Task<Response<GetApplicationByIdResponse>> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            var app = await applicationService.GetApplicationByIdAsync(request.Id);
            if (app == null)
                return NotFound<GetApplicationByIdResponse>();
            var japp_Mapped = mapper.Map<GetApplicationByIdResponse>(app);
            return Success(japp_Mapped);
        }

        public async Task<Response<IReadOnlyList<GetApplicationsResponse>>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
        {
            var apps = await applicationService.GetAllApplicationsAsync();
            var apps_Mapped = mapper.Map<IReadOnlyList<GetApplicationsResponse>>(apps);
            return Success(apps_Mapped, Meta: new { DataCount = apps_Mapped.Count() });
        }
    }
}
