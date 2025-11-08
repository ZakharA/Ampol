using Microsoft.AspNetCore.Mvc;
using LoyaltyAPI.DTOs;
using LoyaltyAPI.Core.Interfaces;
using Shared.Common.Models;
using System.Globalization;
using Asp.Versioning;

namespace LoyaltyAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class LoyaltyController : ControllerBase
{
    private readonly ILoyaltyService _loyaltyService;
    private readonly ILogger<LoyaltyController> _logger;

    public LoyaltyController(
        ILoyaltyService loyaltyService,
        ILogger<LoyaltyController> logger)
    {
        _loyaltyService = loyaltyService;
        _logger = logger;
    }

    /// <summary>
    /// Calculate points for a basket of items
    /// </summary>
    /// <param name="request">Basket items, grand total, and transaction date</param>
    /// <returns>Points earned</returns>
    [HttpPost("calculate")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<PointsResponse>> CalculatePoints([FromBody] PointsRequest request)
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
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            }).ToList();

            // Calculate points
            var result = await _loyaltyService.CalculatePointsAsync(
                basket,
                request.GrandTotal,
                transactionDate);

            // Return response
            return Ok(new PointsResponse
            {
                PointsEarned = result.PointsEarned.ToString()
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating points");
            return StatusCode(500, new { error = "An error occurred while calculating points" });
        }
    }
}