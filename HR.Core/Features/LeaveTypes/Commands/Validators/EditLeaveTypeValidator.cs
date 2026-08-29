using FluentValidation;
using HR.Core.Features.LeaveTypes.Commands.Models;
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

namespace HR.Core.Features.LeaveTypes.Commands.Validators
{
    public class EditLeaveTypeValidator:AbstractValidator<EditLeaveTypeCommand>
    {
        private readonly ILeaveTypeService leaveTypeService;
        private readonly IStringLocalizer<SharedResources> localizer;

        public EditLeaveTypeValidator(ILeaveTypeService leaveTypeService,
            IStringLocalizer<SharedResources> localizer)
        {
            this.leaveTypeService = leaveTypeService;
            this.localizer = localizer;
            ApplyValidationRules();
            ApplyCustomValidationRules();
        }
        public void ApplyValidationRules()
        {
            RuleFor(s => s.Name)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Empty])
                //.NotNull().WithMessage("Name Mustn't Be Null")
                .MaximumLength(20).WithMessage(string.Format(localizer["MaxLength"], 50, localizer["Chars"]));
            RuleFor(s => s.Description)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Empty])
                //.NotNull().WithMessage("Name Mustn't Be Null")
                .MaximumLength(500).WithMessage(string.Format(localizer["MaxLength"], 500, localizer["Chars"]));
            RuleFor(s => s.IsActive)
                .NotNull().WithMessage(localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.IsPaid)
                .NotNull().WithMessage(localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.MaxDaysPerYear)
                .NotNull().WithMessage(localizer[SharedResourcesKeys.Required]);

        }
        public void ApplyCustomValidationRules()
        {
            RuleFor(s => s.Name)
                .MustAsync(async (model,key, cancellationToken) => !await leaveTypeService.IsLeaveTypeExistExcludeSelf(key,model.Id))
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
