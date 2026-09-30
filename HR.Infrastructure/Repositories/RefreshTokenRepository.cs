using HR.Data.Entities;
using HR.Infrastructure.Contexts;
using HR.Infrastructure.Repositories.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly HRAppDbContext context;

        public RefreshTokenRepository(HRAppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(RefreshToken refreshToken,
            CancellationToken cancellationToken = default)
        {
            await context.RefreshTokens.AddAsync(refreshToken,
                cancellationToken);
        }

        public async Task<RefreshToken?> GetByTokenAsync(
            string token,
            CancellationToken cancellationToken = default)
        {
            return await context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == token,cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
