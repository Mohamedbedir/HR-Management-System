using FluentValidation;
using HR.Core.Features.Positions.Commands.Models;
using HR.Core.Localization;
using HR.Service.Services.Contract;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Positions.Commands.Validators
{
    public class EditPositionValidator:AbstractValidator<EditPositionCommand>
    {
        private readonly IPositionService positionService;
        private readonly IStringLocalizer<SharedResources> localizer;

        public EditPositionValidator(IPositionService positionService,
            IStringLocalizer<SharedResources> localizer)
        {
            this.positionService = positionService;
            this.localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(s => s.Title)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Empty])
                //.NotNull().WithMessage("Name Mustn't Be Null")
                .MaximumLength(20).WithMessage(string.Format(localizer["MaxLength"], 50, localizer["Chars"]));
            RuleFor(s => s.Description)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Empty])
                //.NotNull().WithMessage("Name Mustn't Be Null")
                .MaximumLength(500).WithMessage(string.Format(localizer["MaxLength"], 500, localizer["Chars"]));
            RuleFor(s => s.IsActive)
                .NotNull().WithMessage(localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.MinSalary)
                .PrecisionScale(10, 2, true).WithMessage("MinSalary must have up to 10 digits in total and 2 decimal places.");
            RuleFor(x => x.MaxSalary)
                .PrecisionScale(10, 2, true).WithMessage("MaxSalary must have up to 10 digits in total and 2 decimal places.");

        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(s => s.Title)
                .MustAsync(async (model,key, cancellationToken) => !await positionService.IsPositionExistExcludeSelf(key,model.Id))
                .WithMessage(localizer[SharedResourcesKeys.NameExist]);
            //When(d => d.DepartmentId != 0, () =>
            //{
            //    RuleFor(s => s.DepartmentId)
            //    .MustAsync(async (key, cancellationToken) => await departmentService.IsDepartmentIdExist(key))
            //    .WithMessage(localizer[SharedResourcesKeys.IsNotExist]);
            //});


        }
    }
}
