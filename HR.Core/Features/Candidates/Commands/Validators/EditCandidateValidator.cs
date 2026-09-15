using FluentValidation;
using HR.Core.Features.Applications.Commands.Models;
using HR.Core.Features.Candidates.Commands.Models;
using HR.Core.Features.Departments.Commands.Models;
using HR.Core.Features.JobPostings.Commands.Models;
using HR.Core.Localization;
using HR.Service.Services;
using HR.Service.Services.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Candidates.Commands.Validators
{
    public class EditCandidateValidator : AbstractValidator<EditCandidateCommand>
    {
        private readonly IDepartmentService departmentService;
        private readonly IPositionService positionService;
        private readonly IJobPostingService jobPostingService;
        private readonly IStringLocalizer<SharedResources> localizer;

        public EditCandidateValidator(IDepartmentService departmentService,
            IPositionService positionService,
            IJobPostingService jobPostingService,
            IStringLocalizer<SharedResources> localizer)
        {
            this.departmentService = departmentService;
            this.positionService = positionService;
            this.jobPostingService = jobPostingService;
            this.localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(x => x.Id)
            .GreaterThan(0);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(200);

            RuleFor(x => x.Phone)
                .MaximumLength(20)
                .When(x => x.Phone != null);

            RuleFor(x => x.CV)
                .Must(IsValidCV)
                .When(x => x.CV != null)
                .WithMessage("Only PDF, DOC, and DOCX files are allowed.");

            RuleFor(x => x.CV)
                .Must(x => x!.Length <= 5 * 1024 * 1024)
                .When(x => x.CV != null)       
                .WithMessage("CV size must not exceed 5 MB.");
        }
        public void ApplyCustomValidationRules()
        {
            


        }
        private bool IsValidCV(IFormFile? file)
        {
            if (file == null)
                return true;

            var allowedExtensions = new[]
            {
            ".pdf",
            ".doc",
            ".docx"
        };

            var extension =
                Path.GetExtension(file.FileName).ToLowerInvariant();

            return allowedExtensions.Contains(extension);
        }
    }
}
