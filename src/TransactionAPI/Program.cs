using Microsoft.AspNetCore.OpenApi;
using Asp.Versioning;
using TransactionAPI.Core.Interfaces;
using TransactionAPI.Core.Services;
using TransactionAPI.Infra.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITransactionService, TransactionService>();

builder.Services.AddHttpClient<IDiscountService, DiscountService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7201");
});
builder.Services.AddHttpClient<ILoyaltyService, LoyaltyService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7201");
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
