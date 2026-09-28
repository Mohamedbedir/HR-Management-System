using HR.Service.Services.Contract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;

        public bool IsAuthenticated =>
            User?.Identity?.IsAuthenticated ?? false;

        public int? UserId
        {
            get
            {
                var value = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                return int.TryParse(value, out var id)? id : null;
            }
        }

        public int? EmployeeId
        {
            get
            {
                var value = User?.FindFirst("EmployeeId")?.Value;

                return int.TryParse(value, out var id)? id : null;
            }
        }

        public string? Email => User?.FindFirst(ClaimTypes.Email)?.Value;

        public IReadOnlyList<string> Roles => User?.FindAll(ClaimTypes.Role).Select(x => x.Value)
                .ToList()  ?? new List<string>();

        public bool IsInRole(string role)
        {
            return User?.IsInRole(role) ?? false;
        }
    }
}
