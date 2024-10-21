using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RayaBekoIntegration.Core.IServices
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(IEnumerable<Claim> claims);
        Task<string> GenerateRefreshToken(string userId, string accessToken);
        bool IsAccessTokenValid(string token);
        //ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
