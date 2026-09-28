using FluentValidation;
using HR.Core.Features.Authantications.Commands.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Authantications.Commands.Validators
{
    public class LoginValidator : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Please enter a valid email address");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 chars");
                //.Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
                //.Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
                //.Matches("[0-9]").WithMessage("Password must contain at least one one number")
                //.Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special char");
        }
    }
}
