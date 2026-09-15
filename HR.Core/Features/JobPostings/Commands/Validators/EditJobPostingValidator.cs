using FluentValidation;
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

namespace HR.Core.Features.JobPostings.Commands.Validators
{
    public class EditJobPostingValidator : AbstractValidator<EditJobPostingCommand>
    {
        private readonly IDepartmentService departmentService;
        private readonly IPositionService positionService;
        private readonly IJobPostingService jobPostingService;
        private readonly IStringLocalizer<SharedResources> localizer;

        public EditJobPostingValidator(IDepartmentService departmentService,
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

            RuleFor(x => x.Title)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .MaximumLength(2000)
                .When(x => x.Description != null);

            RuleFor(x => x.MinSalary)
                .GreaterThan(0);

            RuleFor(x => x.MaxSalary)
                .GreaterThan(0);

            RuleFor(x => x.MaxSalary)
                .GreaterThanOrEqualTo(x => x.MinSalary);

            RuleFor(x => x.ClosingDate)
                .GreaterThan(DateTime.Now)
                .When(x => x.ClosingDate.HasValue);

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0);

            RuleFor(x => x.PositionId)
                .GreaterThan(0);
        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(s => s.Title)
               .MustAsync(async (model,key, cancellationToken) => !await jobPostingService.IsJobPostingExistExcludeSelf(key,model.Id))
               .WithMessage(localizer[SharedResourcesKeys.NameExist]);

            RuleFor(x => x.DepartmentId)
           .MustAsync(async (departmentId, cancellationToken) =>
               departmentId == null ||
               await departmentService.IsDepartmentExistById(departmentId))
           .WithMessage("Department does not exist.");

            RuleFor(x => x.PositionId)
                .MustAsync(async (positionId, cancellationToken) =>
                    positionId == null ||
                    await positionService.IsPositionExistById(positionId))
                .WithMessage("Position does not exist.");


        }
    }
}
