using FluentValidation;
using HR.Core.Features.Payrolls.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Payrolls.Commands.Validators
{
    public class CalculatePayrollValidator
    : AbstractValidator<CalculatePayrollCommand>
    {
        public CalculatePayrollValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0);

            RuleFor(x => x.Month)
                .InclusiveBetween(1, 12);

            RuleFor(x => x.Year)
                .InclusiveBetween(2000, 2100);

            RuleFor(x => x.Bonus)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Deduction)
                .GreaterThanOrEqualTo(0);
        }
    }
}
