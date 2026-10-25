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
        IRequestHandler<UnLockUserCommand, Response<string>>,
        IRequestHandler<DeleteAppUserCommand, Response<string>>,
        IRequestHandler<EditRolesForUserCommand, Response<string>>,
        IRequestHandler<ChangeUserPasswordCommand, Response<string>>
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserscommandHandler(IStringLocalizer<SharedResources> localizer,
            UserManager<ApplicationUser> userManager) : base(localizer)
        {
            this.userManager = userManager;
        }

        public async Task<Response<string>> Handle(LockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString());
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
            var user = await userManager.FindByIdAsync(request.Id.ToString());
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


        public async Task<Response<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var User = await userManager.FindByIdAsync(request.Id.ToString());
            if (User == null)
                return NotFound<string>();
            var token = await userManager.GeneratePasswordResetTokenAsync(User);
            var res = await userManager.ResetPasswordAsync(User, token,request.NewPassword);
            if (!res.Succeeded)
            {
                var Erorrs = string.Join(Environment.NewLine, res.Errors.Select(e => e.Description));
                return BadRequest<string>(Erorrs);
            }
            return Success("", Message: "Password changed successfully");
        }

        public async Task<Response<string>> Handle(EditRolesForUserCommand request, CancellationToken cancellationToken)
        { 
            var user = await userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return NotFound<string>();

            var currentRoles = await userManager.GetRolesAsync(user);
            var rolesToRemove = currentRoles.Except(request.Roles).ToList();
            var rolesToAdd = request.Roles.Except(currentRoles).ToList();

            if (rolesToRemove.Any())
            {
                var removeResult = await userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!removeResult.Succeeded)
                {
                    var errors = string.Join(Environment.NewLine, removeResult.Errors.Select(e => e.Description));
                    return BadRequest<string>(errors);
                }
            }

            if (rolesToAdd.Any())
            {
                var addResult = await userManager.AddToRolesAsync(user, rolesToAdd);
                if (!addResult.Succeeded)
                {
                    var errors = string.Join(Environment.NewLine, addResult.Errors.Select(e => e.Description));
                    return BadRequest<string>(errors);
                }
            }

            return Success<string>("", Message: "User roles updated successfully");
        }

        public async Task<Response<string>> Handle(DeleteAppUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
                return NotFound<string>();
            var res = await userManager.DeleteAsync(user);
            if (!res.Succeeded)
            {
                return BadRequest<string>("Failed to delete user");
            }
            return Deleted<string>("User deleted successfully");
        }
    }
}
