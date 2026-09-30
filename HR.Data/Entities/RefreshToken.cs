using HR.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Data.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; } = null!;

        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }

        public bool IsRevoked => RevokedAt.HasValue;

        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

        public bool IsActive => !IsRevoked && !IsExpired;

        public int UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;
    }
}
