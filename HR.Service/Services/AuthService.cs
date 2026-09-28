using HR.Data.Entities;
using HR.Infrastructure.Repositories.Contract;
using HR.Service.DTOs;
using HR.Service.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IEmployeeRepo _employeeRepository;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IEmployeeRepo employeeRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeRepository = employeeRepository;
        }

        public async Task<LoginResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return new LoginResult
                {
                    Succeeded = false,
                    ErrorMessage = "Invalid email or password."
                };
            }

            var passwordValid = await _userManager.CheckPasswordAsync(
                user,
                password);

            if (!passwordValid)
            {
                return new LoginResult
                {
                    Succeeded = false,
                    ErrorMessage = "Invalid email or password."
                };
            }

            return new LoginResult
            {
                Succeeded = true,
                User = user
            };
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
                    // 5. Create user
            var user = new ApplicationUser
            {
                UserName = request.Email.Split('@')[0],
                Email = request.Email,
                EmployeeId = request.EmployeeId
            };

            var result = await _userManager.CreateAsync(
                user,
                request.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(x => x.Description));

                throw new Exception(errors);
            }

            // 6. Assign role
            var roleResult = await _userManager.AddToRoleAsync(
                user,
                request.Role);

            if (!roleResult.Succeeded)
            {
                // Rollback user if role assignment fails
                await _userManager.DeleteAsync(user);

                var errors = string.Join(
                    ", ",
                    roleResult.Errors.Select(x => x.Description));

                throw new Exception(errors);
            }
        }
    }
}
