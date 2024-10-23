using Microsoft.AspNetCore.Mvc;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.Core.Models;
using System.Security.Claims;
using System.Collections.Generic;
using RayaBekoIntegration.EF;
using Microsoft.EntityFrameworkCore;
using RayaBekoIntegration.EF.IRepositories;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;


namespace RayaBekoIntegration.WebAPI.Controllers
{
    [EnableRateLimiting("Fixed")]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly ApplicationDbContext _dbContext;
        private readonly IUserService _userService; // Inject user service
        private readonly ITokenService _tokenService; // Inject token service
        private readonly IRefreshTokenService _refreshTokenService; // Inject refresh token service
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        // Constructor with dependency injection
        public AuthController(IUserService userService, ITokenService tokenService, IRefreshTokenService refreshTokenService, IUnitOfWork unitOfWork, IAuthService authService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _refreshTokenService = refreshTokenService;
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var result = await _authService.Login(loginModel);
            if (result == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            return Ok(result);
        }

       


    }
}
