using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.Applications.Commands.Models;
using HR.Core.Features.Departments.Commands.Models;
using HR.Core.Features.JobPostings.Commands.Models;
using HR.Data.Entities;
using HR.Data.Entities.Recruitment;
using HR.Data.Enums;
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

namespace HR.Core.Features.Applications.Commands.Handlers
{
    public class ApplicationCommandHandler : ResponseHandler,
        IRequestHandler<AddApplicationCommand, Response<string>>,
        IRequestHandler<EditApplicationCommand, Response<string>>,
        IRequestHandler<DeleteApplicationCommand, Response<string>>
    {
        private readonly IApplicationService applicationService;
        private readonly IJobPostingService jobPostingService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<SharedResources> localizer;

        public ApplicationCommandHandler(IApplicationService applicationService,
            IJobPostingService jobPostingService,
            IMapper mapper,
            IStringLocalizer<SharedResources> localizer):base(localizer) 
        {
            this.applicationService = applicationService;
            this.jobPostingService = jobPostingService;
            this.mapper = mapper;
            this.localizer = localizer;
        }
        

        public async Task<Response<string>> Handle(AddApplicationCommand request, CancellationToken cancellationToken)
        {
            var jobPosting =
            await jobPostingService.GetJobPostingByIdAsync(request.JobPostingId);

            if (jobPosting == null)
                return NotFound<string>("Job posting not found.");

            if (jobPosting.Status != JobPostingStatus.Published)
            {
                return BadRequest<string>(
                    "Applications can only be submitted for published job postings.");
            }

            var applicationExists =await applicationService.IsApplicationExistsAsync(
                    request.CandidateId,
                    request.JobPostingId);

            if (applicationExists)
            {
                return Conflict<string>(
                    "Candidate has already applied for this job posting.");
            }

            var application = new Application
            {
                CandidateId = request.CandidateId,
                JobPostingId = request.JobPostingId,
                Notes = request.Notes,
                AppliedAt = DateTime.Now,
                Status = ApplicationStatus.Applied
            };

            await applicationService.CreateApplicationAsync(application);

            return Success<string>(
                "Application submitted successfully.");
        }

        public async Task<Response<string>> Handle(EditApplicationCommand request, CancellationToken cancellationToken)
        {
            var application =
        await applicationService.GetApplicationByIdAsync(request.Id);

            if (application == null)
                return NotFound<string>("Application not found.");

            application.Notes = request.Notes;

            await applicationService.UpdateApplicationAsync(application);

            return Success<string>(
                "Application updated successfully.");
        }

        public async Task<Response<string>> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            var application =
        await applicationService.GetApplicationByIdAsync(request.Id);

            if (application == null)
                return NotFound<string>("Application not found.");

            await applicationService.DeleteApplicationAsync(application);

            return Success<string>(
                "Application deleted successfully.");
        }
    }
}
