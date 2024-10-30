using RayaBekoIntegration.Core.Models;
using RayaBekoIntegration.Core.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayaBekoIntegration.Core.IServices
{
    public interface IOrderService
    {
        Task<D365_SalesOrderResponses> CreateOrderFromBekoAsync(BekoOrderRequest bekoOrder);
        SalesOrder MapBekoOrderToRayaOrder(BekoOrderRequest bekoOrder);
        Task<CancelSalesOrderResponse> CancelOrder(int bekoOrderId);
        Task<string> CallingB2CRayaCancelOrderAPI(CancelSalesOrderRequest requestModel);
        Task<string> GetOrderStatus(string orderId);
        Task UpdateOrderStatus(string orderId, string orderStatus);
    }
}
