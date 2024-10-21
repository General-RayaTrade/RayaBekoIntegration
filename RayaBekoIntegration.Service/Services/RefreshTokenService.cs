using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using RayaBekoIntegration.Core.Models;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.EF;
using RayaBekoIntegration.EF.IRepositories;
using RayaBekoIntegration.Services;
using System.Security.Claims;

public class RefreshTokenService : IRefreshTokenService
{
    //private readonly ApplicationDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public RefreshTokenService(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }

    public async Task SaveRefreshToken(RefreshToken token)
    {
        _unitOfWork.refreshTokens.Add(token);
        //_dbContext.RefreshTokens.Add(token);
        //await _dbContext.SaveChangesAsync();
    }

    public async Task<RefreshToken> GetRefreshToken(string username, string password)
    {
        var userData = await _unitOfWork.users.FindAsync(user => user.Username == username && user.Password == password);
        if (userData == null)
        {
            return null;
        }
        return await _unitOfWork.refreshTokens.FindAsync(rt => rt.UserId == userData.Id);
    }

    public async Task UpdateRefreshToken(RefreshToken token)
    {
        //var existingToken = await _dbContext.RefreshTokens
        //                          .AsNoTracking()
        //                          .FirstOrDefaultAsync(rt => rt.UserId == token.UserId && rt.Token == token.Token);
        var existingToken = await _unitOfWork.refreshTokens.FindAsync(rt => rt.UserId == token.UserId && rt.Token == token.Token);

        if (existingToken != null)
        {
            existingToken.ExpiryDate = token.ExpiryDate;
            existingToken.IsRevoked = token.IsRevoked;
            existingToken.Token = token.Token; // Optionally update the token itself
            _unitOfWork.refreshTokens.Update(existingToken);
        }
    }

    public async Task<TokenRequest> CreateNewRefreshToken(RefreshToken token)
    {
        // Find the user associated with the refresh token
        var user = await _unitOfWork.users.FindAsync(users => users.Id == token.UserId);

        if (user == null)
        {
            return null;
        }

        // Create a new list of claims for the user
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username)
    };

        // Generate new access token using the user claims
        var accessToken = await _tokenService.GenerateAccessToken(claims);
        var refreshToken = await _tokenService.GenerateRefreshToken(user.Id.ToString(), accessToken);
        // Update the saved refresh token
        token.ExpiryDate = DateTime.Now.AddMonths(6);
        token.Token = refreshToken;
        token.IsRevoked = false;
        _unitOfWork.refreshTokens.Update(token);
        return new TokenRequest
        {
            RefreshToken = refreshToken,
            AccessToken = accessToken,
        };
    }
}
