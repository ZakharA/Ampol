using Microsoft.AspNetCore.Mvc;
using Shared.Common.DTOs;
using TransactionAPI.Core.Interfaces;
using Asp.Versioning;

namespace TransactionAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;
    private readonly ILogger<TransactionController> _logger;

    public TransactionController(
        ITransactionService transactionService,
        ILogger<TransactionController> logger)
    {
        _transactionService = transactionService;
        _logger = logger;
    }

    /// <summary>
    /// Process a transaction to calculate discounts and points
    /// </summary>
    /// <param name="request">Transaction details</param>
    /// <returns>Transaction summary</returns>
    [HttpPost("process")]
    [MapToApiVersion("1.0")]
    public async Task<ActionResult<TransactionResponse>> ProcessTransaction([FromBody] TransactionRequest request)
    {
        try
        {
            var result = await _transactionService.ProcessTransactionAsync(request);
            if (result == null)
            {
                return BadRequest("Unable to process transaction");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing transaction");
            return StatusCode(500, new { error = "An error occurred while processing transaction" });
        }
    }
}