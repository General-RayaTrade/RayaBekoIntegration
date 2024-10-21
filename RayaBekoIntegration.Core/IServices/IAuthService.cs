using RayaBekoIntegration.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.IServices
{
    public interface IAuthService
    {
        Task<TokenRequest> Login(LoginModel loginModel);
    }
}
