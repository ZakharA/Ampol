using Asp.Versioning;
using LoyaltyAPI.Core.Interfaces;
using LoyaltyAPI.Core.Services;
using LoyaltyAPI.Infra.Repositories;
using Asp.Versioning.Routing;
using FluentValidation;
using FluentValidation.AspNetCore;
using LoyaltyAPI.DTOs;
using LoyaltyAPI.Validators;
using LoyaltyAPI.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ILoyaltyService, LoyaltyService>();
builder.Services.AddScoped<IPointsPromotionRepository, PointsPromotionRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<PointsRequest>, PointsRequestValidator>();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.Configure<RouteOptions>(options =>
{
    options.ConstraintMap.Add("apiVersion", typeof(ApiVersionRouteConstraint));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

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
