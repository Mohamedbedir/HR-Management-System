using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.Users.Queries.Resposes
{
    public class GetAllUsersResponse
    {
        public int Id { get; set; }
        public int? EmployeeId { get; set; }
        public string UserName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IReadOnlyList<string> Roles { get; set; } = null!;
        public bool LockoutEnabled { get; set; } 
        public DateTimeOffset? LockoutEnd { get; set; }
     
    }
}
