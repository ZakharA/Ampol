using Asp.Versioning;
using DiscountAPI.Core.Interfaces;
using DiscountAPI.Core.Services;
using DiscountAPI.Infra.Repositories;
using Asp.Versioning.Routing;
using FluentValidation;
using FluentValidation.AspNetCore;
using DiscountAPI.DTOs;
using DiscountAPI.Validators;
using DiscountAPI.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddScoped<IDiscountPromotionRepository, DiscountPromotionRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddMemoryCache();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new DateTimeOffsetConverter());
});
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<DiscountRequest>, DiscountRequestValidator>();

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
