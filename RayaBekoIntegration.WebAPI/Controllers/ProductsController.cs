using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.Core.Models.Responses;
using RayaBekoIntegration.EF;
using RayaBekoIntegration.Service.Services;
using System.Net.Mime;

namespace RayaBekoIntegration.WebAPI.Controllers
{
    [EnableRateLimiting("Fixed")]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        [HttpGet("GetProducts")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ProductsDetailsResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<object>))]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = _productsService.GetProdcutsDetails();
                return CreateResponse<ProductsDetailsResponse>(new ProductsDetailsResponse
                {
                    products = result,
                    status = true
                }, new ResponseService<ProductsDetailsResponse>()
                    );
            } catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error retrieving Products: {ex.Message}" });
            }
        }
        [HttpGet("GetProduct/{SKU}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ProductDetailsResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<object>))]
        public async Task<IActionResult> GetProduct(string SKU)
        {
            try
            {
                var result = await _productsService.GetProdcutDetails(SKU);
                
                return CreateResponse<ProductDetailsResponse>(new ProductDetailsResponse
                {
                    product = result!,
                    status = true
                }, new ResponseService<ProductDetailsResponse>()
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Error retrieving Product: {ex.Message}" });
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
