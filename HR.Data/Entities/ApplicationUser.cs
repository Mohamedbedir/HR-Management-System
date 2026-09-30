using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int? EmployeeId { get; set; }

        // Navigation
        public Employee? Employee { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }
       = new HashSet<RefreshToken>();
    }
}
