using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.Core.Models;

namespace RayaBekoIntegration.WebAPI.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IRefreshTokenService _refreshTokenService; // Inject refresh token service
        public TokenController(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(LoginModel loginModel)
        {
            // Fetch saved refresh token from the database
            var savedRefreshToken = await _refreshTokenService.GetRefreshToken(loginModel.Username, loginModel.Password);

            if (savedRefreshToken == null || savedRefreshToken.IsRevoked)
            {
                return Unauthorized("Invalid refresh token.");
            }
            var result = await _refreshTokenService.CreateNewRefreshToken(savedRefreshToken);
            if (result == null)
            {
                return Unauthorized("Invalid user.");
            }

            return Ok(result);
        }
    }
}
