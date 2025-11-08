using Microsoft.AspNetCore.Mvc;
using DiscountAPI.DTOs;
using DiscountAPI.Core.Interfaces;
using Shared.Common.Models;
using Asp.Versioning;
using DiscountAPI.Mapping;

namespace DiscountAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;
    private readonly ILogger<DiscountController> _logger;

    public DiscountController(
        IDiscountService discountService,
        ILogger<DiscountController> logger)
    {
        _discountService = discountService;
        _logger = logger;
    }

    /// <summary>
    /// Calculate discount for a basket of items
    /// </summary>
    /// <param name="request">Basket items and transaction date</param>
    /// <returns>Total amount, discount applied, and grand total</returns>
    [HttpPost("calculate")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<DiscountResponse>> CalculateDiscount([FromBody] DiscountRequest request)
    {
        try
        {
            // Convert DTOs to domain models
            var basket = request.Basket.ToDomain();

            // Calculate discount
            var result = await _discountService.CalculateDiscountAsync(basket, request.TransactionDate);

            // Return response
            return Ok(new DiscountResponse
            {
                TotalAmount = result.TotalAmount,
                DiscountApplied = result.DiscountApplied,
                GrandTotal = result.GrandTotal
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating discount");
            return StatusCode(500, new { error = "An error occurred while calculating discount" });
        }
    }
}