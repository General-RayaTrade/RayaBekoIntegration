using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.WebAPI.ResonsesModelViews;
using System.Net.Mime;

namespace RayaBekoIntegration.WebAPI.Controllers
{
    [EnableRateLimiting("Fixed")]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class DistrictsController : ControllerBase
    {
        private readonly IDistrictService _districtService;
        public DistrictsController(IDistrictService districtService)
        {
            _districtService = districtService;
        }
        [HttpGet("GetDistricts")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Get() 
        {
            try
            {
                var result = _districtService.GetAllCityDistricts();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error creating order: {ex.Message}"
                });
            }
        }
    }
}
