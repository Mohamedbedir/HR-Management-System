using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Repositories.Contract
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken,
            CancellationToken cancellationToken = default);

        Task<RefreshToken?> GetByTokenAsync(string token,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
