using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface ICurrentUserService
    {
        int? UserId { get; }

        int? EmployeeId { get; }

        string? Email { get; }

        IReadOnlyList<string> Roles { get; }

        bool IsAuthenticated { get; }

        bool IsInRole(string role);
    }
}
