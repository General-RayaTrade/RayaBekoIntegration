using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.EF;
using RayaBekoIntegration.EF.IRepositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RayaBekoIntegration.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TokenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a_secure_key_for_jwt_tokens_123456"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "YourIssuer",
                audience: "YourAudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: creds
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Save access token details to the database (optional)
            var userId = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _unitOfWork.users.FindAsync(u => u.Id == int.Parse(userId));
            if (user != null)
            {
                user.AccessToken = accessToken;
                user.AccessTokenExpiry = token.ValidTo;
                _unitOfWork.users.Update(user);
            }

            return accessToken;
        }

        public async Task<string> GenerateRefreshToken(string userId, string accessToken)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                UserId = int.Parse(userId),
                ExpiryDate = DateTime.Now.AddMonths(6),
                IsRevoked = false,
            };
            return refreshToken.Token;
        }
        public bool IsAccessTokenValid(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;

                // Check if token is expired
                if (jwtToken != null && jwtToken.ValidTo > DateTime.UtcNow)
                {
                    return true; // Token is valid
                }
            }
            catch
            {
                return false; // Token is not valid
            }
            return false; // Token is expired or invalid
        }
    }
}
