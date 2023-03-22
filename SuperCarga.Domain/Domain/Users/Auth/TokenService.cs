using Microsoft.IdentityModel.Tokens;
using SuperCarga.Application.Domain.Users.Dto;
using SuperCarga.Application.Domain.Users.Models;
using SuperCarga.Application.Exceptions;
using SuperCarga.Application.Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SuperCarga.Domain.Domain.Users.Auth
{
    public class TokenService
    {
        private readonly JwtConfig config;

        public TokenService(JwtConfig config)
        {
            this.config = config;
        }

        public TokenDto GenerateTokens(User user)
        {
            var now = DateTime.UtcNow;

            var result = new TokenDto
            {
                AccessToken = GenerateAccessToken(now, user.Email, user.Roles.Select(x => x.Name).ToList()),
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiry = now.AddDays(config.RefreshTokenExpiration)
            };

            return result;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public string GenerateAccessToken(DateTime now, string email, List<string> roles)
        {
            var expiry = now.AddMinutes(config.AccessTokenExpiration);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, email));

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var token = new JwtSecurityToken(
                issuer: config.Issuer,
                audience: config.Audience,
                claims: claims,
                notBefore: now,
                expires: expiry,
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = Key,
                ValidateLifetime = false,
                ValidAudience = config.Audience,
                ValidIssuer = config.Issuer,
                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new BadRequestException("Invalid token");

            return principal;
        }

        private SymmetricSecurityKey Key => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.Secret));

    }
}
