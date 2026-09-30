using HR.Data.AppMetaData;
using HR.Data.Entities;
using HR.Infrastructure.Repositories.Contract;
using HR.Service.Services.Contract;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly JwtSettings jwtSettings;
        //private const int RefreshTokenExpirationDays = 7;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository,
            IOptions<JwtSettings> jwtSettings)
        {
            this.refreshTokenRepository = refreshTokenRepository;
            this.jwtSettings = jwtSettings.Value;
        }

        public string GenerateToken()
        {
            var randomBytes = RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(randomBytes);
        }

        public async Task<RefreshToken> CreateAsync(ApplicationUser user,
            CancellationToken cancellationToken = default)
        {
            var refreshToken = new RefreshToken
            {
                Token = GenerateToken(),

                ExpiresAt = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpirationDays),

                UserId = user.Id,

                //CreatedAt = DateTime.UtcNow
            };

            await refreshTokenRepository.AddAsync(refreshToken, cancellationToken);

            //await refreshTokenRepository.SaveChangesAsync(cancellationToken);

            return refreshToken;
        }

        public async Task<RefreshToken?> GetActiveTokenAsync(string token,
            CancellationToken cancellationToken = default)
        {
            var refreshToken = await refreshTokenRepository.GetByTokenAsync(token, cancellationToken);

            if (refreshToken is null)
                return null;

            return refreshToken.IsActive ? refreshToken : null;
        }

        public async Task RevokeAsync(RefreshToken refreshToken,
            CancellationToken cancellationToken = default)
        {
            refreshToken.RevokedAt = DateTime.UtcNow;

            await refreshTokenRepository.SaveChangesAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await refreshTokenRepository.SaveChangesAsync(
                cancellationToken);
        }
    }
}
