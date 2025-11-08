using Microsoft.AspNetCore.Mvc;
using DiscountAPI.DTOs;
using DiscountAPI.Core.Interfaces;
using Shared.Common.Models;
using System.Globalization;
using Asp.Versioning;

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
            // Parse transaction date
            var transactionDate = DateTime.ParseExact(
                request.TransactionDate,
                "dd-MMM-yyyy",
                CultureInfo.InvariantCulture);

            // Convert DTOs to domain models
            var basket = request.Basket.Select(item => new BasketItem
            {
                ProductId = item.ProductId,
                UnitPrice = decimal.Parse(item.UnitPrice),
                Quantity = int.Parse(item.Quantity)
            }).ToList();

            // Calculate discount
            var result = await _discountService.CalculateDiscountAsync(basket, transactionDate);

            // Return response
            return Ok(new DiscountResponse
            {
                TotalAmount = result.TotalAmount.ToString("F2"),
                DiscountApplied = result.DiscountApplied.ToString("F2"),
                GrandTotal = result.GrandTotal.ToString("F2")
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating discount");
            return StatusCode(500, new { error = "An error occurred while calculating discount" });
        }
    }
}