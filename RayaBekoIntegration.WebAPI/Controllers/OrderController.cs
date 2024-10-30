using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.Core.Models;
using RayaBekoIntegration.Core.Models.Responses;
using RayaBekoIntegration.Service.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RayaBekoIntegration.WebAPI.Controllers
{
    [EnableRateLimiting("Fixed")]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IVendorService _vendorService;

        public OrderController(IOrderService orderService, IVendorService vendorService)
        {
            _orderService = orderService;
            _vendorService = vendorService;
        }

        [HttpPost("create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<D365_SalesOrderResponses>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<object>))]
        [SwaggerOperation(
            Summary = "Create Order",
            Description = "Creates an order in Raya's system based on Beko's order request."
        )]
        public async Task<IActionResult> CreateOrder([FromBody] BekoOrderRequest bekoOrder)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (bekoOrder == null)
                return BadRequest(new { success = false, message = "Invalid order data." });

            try
            {
                var orderResponse = await _orderService.CreateOrderFromBekoAsync(bekoOrder);
                return CreateResponse<D365_SalesOrderResponses>(orderResponse, new ResponseService<D365_SalesOrderResponses>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error creating order: {ex.Message}" });
            }
        }

        [HttpPost("cancel/{orderNumber}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<CancelSalesOrderResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<object>))]
        [SwaggerOperation(
            Summary = "Cancel Order",
            Description = "Cancels an order in Raya's system based on the order number provided."
        )]
        public async Task<IActionResult> CancelOrder(int orderNumber)
        {
            if (orderNumber <= 0)
                return BadRequest(new { success = false, message = "Invalid order number." });

            try
            {
                var cancelResponse = await _orderService.CancelOrder(orderNumber);
                return CreateResponse<CancelSalesOrderResponse>(cancelResponse, new ResponseService<CancelSalesOrderResponse>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error cancelling order: {ex.Message}" });
            }
        }

        [HttpGet("status/{orderId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<OrderStatusResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<object>))]
        [SwaggerOperation(
            Summary = "Get Order Status",
            Description = "Retrieves the status of an order in Raya's system using the order ID."
        )]
        public async Task<IActionResult> GetOrderStatus(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
                return BadRequest(new { success = false, message = "Order ID is required." });

            try
            {
                var orderStatus = await _orderService.GetOrderStatus(orderId);
                return CreateResponse<OrderStatusResponse>(new OrderStatusResponse {
                    BekoOrderId = orderId,
                    items = new List<Item>(),
                    status = true
                }, new ResponseService<OrderStatusResponse>()
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error retrieving order status: {ex.Message}" });
            }
        }
        [HttpPut("Update-Status/{orderId}/{orderStatus}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<UpdateOrderStatusResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<object>))]
        [SwaggerOperation(
            Summary = "Update Order Status",
            Description = "Updates the status of an order in Raya's system and Beko using the order ID."
        )]
        public async Task<IActionResult> UpdateOrderStatus(string orderId, string orderStatus)
        {
            if (string.IsNullOrWhiteSpace(orderId))
                return BadRequest(new { success = false, message = "Order ID is required." });

            try
            {
                await _orderService.UpdateOrderStatus(orderId, orderStatus);
                var result = await _vendorService.UpdateOrderStatus(orderId, orderStatus);
                return CreateResponse<UpdateOrderStatusResponse>(new UpdateOrderStatusResponse
                {
                    BekoOrderId = orderId,
                    OrderStatus = orderStatus,
                    status = true
                }, new ResponseService<UpdateOrderStatusResponse>()
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error update order: {ex.Message}" });
            }
        }
        private IActionResult CreateResponse<T>(T response, IResponseService<T> _responseService) where T : class, IStatus
        {
            if (response == null || !response.status == null)
                return BadRequest(_responseService.CreateResponse(statusCode: 400, payload: response));

            return Ok(_responseService.CreateResponse(statusCode: 200, payload: response));
        }
    }
}
