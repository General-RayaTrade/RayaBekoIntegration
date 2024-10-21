using RayaBekoIntegration.Core.Models;
using RayaBekoIntegration.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.IServices
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
    }
}
