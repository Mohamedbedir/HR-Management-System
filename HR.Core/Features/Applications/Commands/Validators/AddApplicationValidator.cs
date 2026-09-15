using FluentValidation;
using HR.Core.Features.Applications.Commands.Models;
using HR.Core.Features.Departments.Commands.Models;
using HR.Core.Features.JobPostings.Commands.Models;
using HR.Core.Localization;
using HR.Service.Services;
using HR.Service.Services.Contract;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Applications.Commands.Validators
{
    public class AddApplicationValidator : AbstractValidator<AddApplicationCommand>
    {
        private readonly IApplicationService applicationService;
        private readonly IJobPostingService jobPostingService;
        private readonly IStringLocalizer<SharedResources> localizer;

        public AddApplicationValidator(
            IApplicationService  applicationService ,
            IJobPostingService jobPostingService,
            IStringLocalizer<SharedResources> localizer)
        {
            
            this.applicationService = applicationService;
            this.jobPostingService = jobPostingService;
            this.localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.CandidateId)
           .GreaterThan(0);

            RuleFor(x => x.JobPostingId)
                .GreaterThan(0);

            RuleFor(x => x.Notes)
                .MaximumLength(1000)
                .When(x => x.Notes != null);
        }
        public void ApplyCustomValidationRules()
        {
            

            RuleFor(x => x.JobPostingId)
           .MustAsync(async (jobPostingId, cancellationToken) =>
               jobPostingId == null ||
               await jobPostingService.IsJobPostingExistById(jobPostingId))
           .WithMessage("Job posting does not exist.");

            //RuleFor(x => x.CandidateId)
            //    .MustAsync(async (candidateId, cancellationToken) =>
            //        candidateId == null ||
            //        await candidateService.IsCandidateExistById(candidateId))
            //    .WithMessage("Candidate does not exist.");



        }
    }
}
