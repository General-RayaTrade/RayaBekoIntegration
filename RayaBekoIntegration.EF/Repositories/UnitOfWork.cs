using RayaBekoIntegration.EF.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.EF.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ApplicationDbContext context { get; }
        public IBaseRepository<RefreshToken> refreshTokens { get; set; }
        public IBaseRepository<User> users { get; set; }
        public IBaseRepository<Order> orders { get; set; }
        public IBaseRepository<OrderDetail> orderDetails { get; set; }
        public IBaseRepository<OrderStatusLog> orderStatusLogs { get; set; }
        public IBaseRepository<VWcityDistrict> vWCityDistricts { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            refreshTokens = new BaseRepository<RefreshToken>(context);
            users = new BaseRepository<User>(context);
            orders = new BaseRepository<Order>(context);
            orderDetails = new BaseRepository<OrderDetail>(context);
            orderStatusLogs = new BaseRepository<OrderStatusLog>(context);
            vWCityDistricts = new BaseRepository<VWcityDistrict>(context);
        }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
