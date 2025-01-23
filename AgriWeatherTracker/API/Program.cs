using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AgriWeatherTracker.Data;
using AutoMapper;
using System.Reflection;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AgriWeatherTracker.Service;

// Enable legacy timestamp behavior
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Retrieve Key Vault Endpoint from app settings
var keyVaultEndpoint = builder.Configuration["KeyVault:VaultUri"];

if (!string.IsNullOrEmpty(keyVaultEndpoint))
{
    // Add Azure Key Vault to the configuration pipeline
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultEndpoint), new DefaultAzureCredential());
}

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ICropRepository, CropRepository>();
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IConditionThresholdRepository, ConditionThresholdRepository>();
builder.Services.AddScoped<IGrowthCycleRepository, GrowthCycleRepository>();
builder.Services.AddScoped<IGrowthStageRepository, GrowthStageRepository>();
builder.Services.AddScoped<IHealthScoreRepository, HealthScoreRepository>();
builder.Services.AddScoped<IWeatherEvaluator, HumidityEvaluator>();
builder.Services.AddScoped<IWeatherEvaluator, WindEvaluator>();
builder.Services.AddScoped<IWeatherEvaluator, TemperatureEvaluator>();
builder.Services.AddScoped<IWeatherEvaluator, RainfallEvaluator>();
builder.Services.AddScoped<WeatherHealthService>();
builder.Services.AddScoped<HealthEvaluatorService>();
builder.Services.AddTransient<IEmailService, SendGridEmailService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure PostgreSQL Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
