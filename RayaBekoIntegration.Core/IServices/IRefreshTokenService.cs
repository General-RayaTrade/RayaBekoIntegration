using RayaBekoIntegration.Core.Models;
using RayaBekoIntegration.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.IServices
{
    public interface IRefreshTokenService
    {
        Task SaveRefreshToken(RefreshToken token);
        Task<RefreshToken> GetRefreshToken(string userId, string token);
        Task UpdateRefreshToken(RefreshToken token);
        Task<TokenRequest> CreateNewRefreshToken(RefreshToken token);
    }
}
