using RayaBekoIntegration.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Service.Services
{
    public class VendorService : IVendorService
    {
        public async Task<string> UpdateOrderStatus(string orderId, string orderStatus)
        {
            return "Done";
            //throw new NotImplementedException();
        }
    }
}
