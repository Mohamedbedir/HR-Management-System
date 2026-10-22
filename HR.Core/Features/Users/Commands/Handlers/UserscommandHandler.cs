using HR.Core.Bases;
using HR.Core.Features.Users.Commands.Models;
using HR.Core.Localization;
using HR.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Users.Commands.Handlers
{
    public class UserscommandHandler : ResponseHandler, 
        IRequestHandler<LockUserCommand, Response<string>>,
        IRequestHandler<UnLockUserCommand, Response<string>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserscommandHandler(IStringLocalizer<SharedResources> localizer,
            UserManager<ApplicationUser> userManager) : base(localizer)
        {
            this.userManager = userManager;
        }

        public async Task<Response<string>> Handle(LockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user == null)
                return NotFound<string>();
            var res = await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now.AddHours(5));
            if (!res.Succeeded)
            {
                var errors = string.Join(Environment.NewLine,
                                         res.Errors.Select(e => e.Description));

                return BadRequest<string>(errors);
            }
            return Success<string>("", Message: "User locked successfully");
        }

        public async Task<Response<string>> Handle(UnLockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id);
            if (user == null)
                return NotFound<string>();
            var res = await userManager.SetLockoutEndDateAsync(user, null);
            if (!res.Succeeded)
            {
                var errors = string.Join(Environment.NewLine,
                                         res.Errors.Select(e => e.Description));

                return BadRequest<string>(errors);
            }
            return Success<string>("", Message: "User Unlocked successfully");
        }
    }
}
