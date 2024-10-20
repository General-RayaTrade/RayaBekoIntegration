﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RayaBekoIntegration.Core.IServices;
using RayaBekoIntegration.Core.Models;
using RayaBekoIntegration.Service.Services;
using RayaBekoIntegration.WebAPI.ResonsesModelViews;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RayaBekoIntegration.WebAPI.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("Create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<ProductStockResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<object>))]
        [SwaggerOperation(
            Summary = "Create an order based on Beko request",
            Description = "Creates an order in Raya's system from the Beko order details provided."
        )]
        public async Task<IActionResult> CreateOrder([FromBody] BekoOrderRequest bekoOrder)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Validate incoming BEKO order
                if (bekoOrder == null)
                {
                    return BadRequest("Invalid order data.");
                }

                // Call service to map and create the order in Raya's system
                var createdOrder = await _orderService.CreateOrderFromBekoAsync(bekoOrder);

                // Return success response
                return Ok(new
                {
                    success = true,
                    message = "Order created successfully",
                    orderNumber = createdOrder.M_OrderNumber
                });
            }
            catch (Exception ex)
            {
                // Handle errors and return appropriate response
                return StatusCode(500, new
                {
                    success = false,
                    message = $"Error creating order: {ex.Message}"
                });
            }
        }

        //[HttpPost("return-status/{orderId}")]
        //[Produces("application/json")]
        //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<ProductStockResponse>))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<object>))]
        //[SwaggerOperation(
        //    Summary = "Create an order based on Beko request",
        //    Description = "Creates an order in Raya's system from the Beko order details provided."
        //)]
        //public async Task<IActionResult> UpdateReturnStatus(int orderId, [FromBody] BekoOrderRequest bekoOrder)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        // Validate incoming BEKO order
        //        if (bekoOrder == null)
        //        {
        //            return BadRequest("Invalid order data.");
        //        }

        //        // Call service to map and create the order in Raya's system
        //        var createdOrder = await _orderService.CreateOrderFromBekoAsync(bekoOrder);

        //        // Return success response
        //        return Ok(new
        //        {
        //            success = true,
        //            message = "Order created successfully",
        //            orderNumber = createdOrder.M_OrderNumber
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle errors and return appropriate response
        //        return StatusCode(500, new
        //        {
        //            success = false,
        //            message = $"Error creating order: {ex.Message}"
        //        });
        //    }
        //}

    }
}