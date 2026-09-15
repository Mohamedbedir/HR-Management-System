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
    public class EditApplicationValidator : AbstractValidator<EditApplicationCommand>
    {
        private readonly IDepartmentService departmentService;
        private readonly IPositionService positionService;
        private readonly IJobPostingService jobPostingService;
        private readonly IStringLocalizer<SharedResources> localizer;

        public EditApplicationValidator(IDepartmentService departmentService,
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

            RuleFor(x => x.Notes)
                .MaximumLength(1000)
                .When(x => x.Notes != null);
        }
        public void ApplyCustomValidationRules()
        {
            


        }
    }
}
