using Microsoft.AspNetCore.Mvc;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.Core.Models;
using RayaBekoIntegration.WebAPI.ResonsesModelViews;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using RayaBekoIntegration.Core.Models.Responses;
using RayaBekoIntegration.Service.Services;

namespace RayaBekoIntegration.WebAPI.Controllers
{
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        private readonly ITokenService _tokenService;
        private readonly IResponseService<ProductStockResponse> _responseService;
        public StockController(IStockService stockService, ITokenService tokenService)
        {
            _stockService = stockService;
            _tokenService = tokenService;
            _responseService = new ResponseService<ProductStockResponse>();

        }
        [HttpGet("{productItemCode}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ProductStockResponse>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<object>))]
        [SwaggerOperation(
            Summary = "Retrieve stock details for a specific product",
            Description = "Returns the stock quantity and inventory details for a given product identified by the productItemCode. If the product is not found, a 404 status is returned."
        )]
        public async Task<IActionResult> GetStock(string productItemCode)
        {
            // Extract the access token from the request (usually from Authorization header)
            var accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            // Validate the access token
            if (!_tokenService.IsAccessTokenValid(accessToken))
            {
                return Unauthorized("Access token is expired or invalid.");
            }
            IList<ItemInventoryQuantity>? stock = await _stockService.GetStockForProductAsync(productItemCode);
            if (stock == null) return NotFound($"Product item code [{productItemCode}] not found.");

            var payload = new ProductStockResponse
            {
                productItemCode = productItemCode,
                Details = stock
                        .Where(st => st != null) // Ensure no null entries
                        .Select(st => new ItemInventoryQuantity
                        {
                            quantity = st.quantity,
                            inventory = st.inventory,
                        })
                        .ToList()
            };
            var model = _responseService.CreateResponse(statusCode: 200, payload: payload);
            return Ok(model);
        }
    }
}
