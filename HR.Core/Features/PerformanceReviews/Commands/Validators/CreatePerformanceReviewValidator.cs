using FluentValidation;
using HR.Core.Features.PerformanceReviews.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.PerformanceReviews.Commands.Validators
{
    public class CreatePerformanceReviewValidator
    : AbstractValidator<CreatePerformanceReviewCommand>
    {
        public CreatePerformanceReviewValidator()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0);

            RuleFor(x => x.ReviewerId)
                .GreaterThan(0);

            RuleFor(x => x.Month)
                .InclusiveBetween(1, 12);

            RuleFor(x => x.Year)
                .InclusiveBetween(2000, 2100);

            RuleFor(x => x.Score)
                .InclusiveBetween(1, 5);

            RuleFor(x => x.Comments)
                .MaximumLength(2000)
                .When(x => x.Comments != null);
        }
    }
}
