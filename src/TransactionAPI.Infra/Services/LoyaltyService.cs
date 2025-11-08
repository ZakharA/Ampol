using System.Text;
using System.Text.Json;
using Shared.Common.DTOs;
using TransactionAPI.Core.DTOs;
using TransactionAPI.Core.Interfaces;

namespace TransactionAPI.Infra.Services;

public class LoyaltyService : ILoyaltyService
{
    private readonly HttpClient _httpClient;

    public LoyaltyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<PointsResponse?> CalculatePointsAsync(TransactionRequest request, string grandTotal)
    {
        var pointsRequest = new
        {
            request.TransactionDate,
            GrandTotal = grandTotal,
            request.Basket
        };

        var json = JsonSerializer.Serialize(pointsRequest);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("api/v1/loyalty/calculate", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<PointsResponse>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}