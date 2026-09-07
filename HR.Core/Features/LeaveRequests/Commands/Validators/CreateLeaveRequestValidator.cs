using FluentValidation;
using HR.Core.Features.LeaveRequests.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.LeaveRequests.Commands.Validators
{
    public class CreateLeaveRequestValidator : AbstractValidator<CreateLeaveRequestCommand>
    {
        public CreateLeaveRequestValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithMessage("EmployeeId must be greater than 0.");

            RuleFor(x => x.LeaveTypeId)
                .GreaterThan(0)
                .WithMessage("LeaveTypeId must be greater than 0.");

            RuleFor(x => x.StartDate)
                .NotEmpty()
                .WithMessage("Start date is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty()
                .WithMessage("End date is required.");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate)
                .WithMessage("End date must be greater than or equal to start date.");

            RuleFor(x => x.Reason)
                .MaximumLength(1000)
                .When(x => x.Reason != null);
        }
    }
}
