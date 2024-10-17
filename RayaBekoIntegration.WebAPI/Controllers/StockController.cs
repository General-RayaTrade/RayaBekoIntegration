using Microsoft.AspNetCore.Mvc;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.Core.Models;
using RayaBekoIntegration.WebAPI.ResonsesModelViews;
using System.Net.Mime;
using Swashbuckle.AspNetCore.Annotations;

namespace RayaBekoIntegration.WebAPI.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;
        public StockController(IStockService stockService)
        {
            _stockService = stockService;
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
            IList<ItemInventoryQuantity>? stock = await _stockService.GetStockForProductAsync(productItemCode);
            if (stock == null) return NotFound($"Product item code [{productItemCode}] not found.");

            Response<ProductStockResponse> model = new Response<ProductStockResponse>
            {
                StatusCode = 200,
                Payload = new ProductStockResponse
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
                }
            };
            return Ok(model);
        }
    }
}
