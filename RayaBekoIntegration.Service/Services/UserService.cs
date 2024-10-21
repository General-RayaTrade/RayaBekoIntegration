using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.Core.Models;
using RayaBekoIntegration.EF;
using RayaBekoIntegration.EF.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace RayaBekoIntegration.Services
{
    public class UserService : IUserService
    {
        //// This should be replaced with actual user data retrieval logic (e.g., from a database).
        //private readonly List<User> _users = new List<User>
        //{
        //    new User { Id = 1, Username = "user1", Password = "password1" },
        //    new User { Id = 2, Username = "user2", Password = "password2" }
        //};
        //private readonly ApplicationDbContext _dbContext;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<User> Authenticate(string username, string password)
        {
            //return _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            return await _unitOfWork.users.FindAsync(u => u.Username == username && u.Password == password);
        }
    }
}
