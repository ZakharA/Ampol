using Microsoft.AspNetCore.Mvc;
using LoyaltyAPI.DTOs;
using LoyaltyAPI.Core.Interfaces;
using Shared.Common.Models;
using Asp.Versioning;
using LoyaltyAPI.Mapping;

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
            // Convert DTOs to domain models
            var basket = request.Basket.ToDomain();

            // Calculate points
            var result = await _loyaltyService.CalculatePointsAsync(
                basket,
                request.GrandTotal,
                request.TransactionDate);

            // Return response
            return Ok(new PointsResponse
            {
                PointsEarned = result.PointsEarned,
                PromotionApplied = result.PromotionApplied
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calculating points");
            return StatusCode(500, new { error = "An error occurred while calculating points" });
        }
    }
}