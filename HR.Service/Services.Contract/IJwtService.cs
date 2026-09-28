using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IJwtService
    {
        Task<(string Token, DateTime ExpiresAt)> GenerateTokenAsync(
            ApplicationUser user);
    }
}
