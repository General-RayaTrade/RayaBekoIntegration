using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.EF.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationDbContext context { get; }
        IBaseRepository<RefreshToken> refreshTokens {  get; }
        IBaseRepository<User> users {  get; }
        IBaseRepository<Order> orders {  get; }
        IBaseRepository<OrderDetail> orderDetails {  get; }
        IBaseRepository<OrderStatusLog> orderStatusLogs {  get; }
        int Complete();
    }
}
