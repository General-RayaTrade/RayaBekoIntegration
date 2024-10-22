using Microsoft.EntityFrameworkCore;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.Core.Models;
using RayaBekoIntegration.EF.IRepositories;
using RayaBekoIntegration.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService; // Inject token service
        private readonly IUserService _userService; // Inject user service
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(ITokenService tokenService, IUserService userService, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _userService = userService;
            _unitOfWork = unitOfWork;

        }
        public async Task<TokenRequest> Login(LoginModel loginModel)
        { 
            // Authenticate the user
            var user = await _userService.Authenticate(loginModel.Username, loginModel.Password);

            if (user == null)
            {
                return null;
            }

            // Create claims for the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };

            // Generate tokens
            var accessToken = await _tokenService.GenerateAccessToken(claims);
            var refreshToken = (await _unitOfWork.refreshTokens.FindAsync(refresh => refresh.Id == user.Id))!.Token;

            //user.AccessToken = accessToken;
            //user.AccessTokenExpiry = DateTime.Now.AddMinutes(15);
            //_unitOfWork.users.Update(user);
            return new TokenRequest
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
    }
}
