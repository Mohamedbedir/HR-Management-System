using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IRefreshTokenService
    {
        string GenerateToken();

        Task<RefreshToken> CreateAsync( ApplicationUser user,
            CancellationToken cancellationToken = default);

        Task<RefreshToken?> GetActiveTokenAsync( string token,
            CancellationToken cancellationToken = default);

        Task RevokeAsync(RefreshToken refreshToken,
            CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
