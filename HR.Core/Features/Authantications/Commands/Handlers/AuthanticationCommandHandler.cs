using Azure;
using HR.Core.Bases;
using HR.Core.Features.Authantications.Commands.Models;
using HR.Core.Features.Authantications.Commands.Respnses;
using HR.Data.Entities;
using HR.Service.DTOs;
using HR.Service.Services;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Authantications.Commands.Handlers
{
    public class AuthanticationCommandHandler : ResponseHandler,
        IRequestHandler<RegisterCommand, Bases.Response<string>>,
        IRequestHandler<LoginCommand, Bases.Response<LoginResponse>>
    {
        private readonly IAuthService authService;
        private readonly RoleManager<IdentityRole<int>> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmployeeService employeeService;
        private readonly IJwtService jwtService;
        private readonly IStringLocalizer<SharedResources> localizer;

        public AuthanticationCommandHandler(IAuthService _authService,
            RoleManager<IdentityRole<int>> roleManager,
            UserManager<ApplicationUser> userManager,
            IEmployeeService employeeService,
            IJwtService jwtService,
            IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            this.authService = _authService;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.employeeService = employeeService;
            this.jwtService = jwtService;
            this.localizer = localizer;
        }

       
        public async Task<Bases.Response<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Employee Not Found
            var employeeExists =
                await employeeService.IsEmployeeExistById(request.EmployeeId);

            if (!employeeExists)
                return NotFound<string>("Employee not found.");

            //Check Employee already has account
            var existingUser = await userManager.Users
                .FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId);

            if (existingUser is not null)
                return BadRequest<string>("This employee already has an account.");



            // Email already exists
            var emailExists =await userManager.FindByEmailAsync(request.Email);

            if (emailExists is not null)
                return BadRequest<string>("Email already exists.");
            /* var emailExists = await userManager.Users.AnyAsync(s=>s.Email==request.Email)*/
            ;
            //if (emailExists)
            //    return BadRequest<string>("Email already exists.");

            // Role doesn't exist
            if (!await roleManager.RoleExistsAsync(request.Role))
                return BadRequest<string>("Role does not exist.");

            // Create account
            var registerRequest = new RegisterRequest
            {
                EmployeeId = request.EmployeeId,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role
            };

            try
            {
                await authService.RegisterAsync(registerRequest);

                return Success("Account created successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest<string>(ex.Message);
            }
        }

        public async Task<Bases.Response<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await authService.LoginAsync(request.Email,request.Password);

            if (!result.Succeeded)
                return BadRequest<LoginResponse>(
                    result.ErrorMessage!);

            var (token, expiresAt) = await jwtService.GenerateTokenAsync( result.User!);

            var response = new LoginResponse
            {
                AccessToken = token,
                ExpiresAt = expiresAt
            };

            return Success<LoginResponse>(response,Message:"Login successful.");
        }
    }
}
