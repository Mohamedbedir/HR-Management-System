using HR.Data.AppMetaData;
using HR.Data.Entities;
using HR.Service.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class JwtService : IJwtService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtSettings jwtSettings;

        public JwtService(UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings)
        {
            this.userManager = userManager;
            this.jwtSettings = jwtSettings.Value;
        }

        public async Task<(string Token, DateTime ExpiresAt)> GenerateTokenAsync(
            ApplicationUser user)
        {
            var roles = await userManager.GetRolesAsync(user);

            var expiresAt = DateTime.UtcNow.AddMinutes(jwtSettings.AccessTokenExpirationMinutes);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(ClaimTypes.Email,user.Email ?? string.Empty)
            };

            // EmployeeId
            if (user.EmployeeId.HasValue)
            {
                claims.Add(new Claim("EmployeeId",user.EmployeeId.Value.ToString()));
            }

            // Roles
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            var tokenString =new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenString, expiresAt);
        }
    }
}
