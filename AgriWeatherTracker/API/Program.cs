using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AgriWeatherTracker.Data;
using AutoMapper;
using System.Reflection;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AgriWeatherTracker.Service;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Enable legacy timestamp behavior for Npgsql
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add logging to verify initial configuration
var loggerFactory = builder.Services.BuildServiceProvider().GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger<Program>();

// Log initial configuration
logger.LogInformation("Starting application configuration...");

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Retrieve Key Vault Endpoint from app settings
var keyVaultEndpoint = builder.Configuration["KeyVault:VaultUri"];
logger.LogInformation($"Key Vault Endpoint: {keyVaultEndpoint}");

if (!string.IsNullOrEmpty(keyVaultEndpoint))
{
    // Add Azure Key Vault to the configuration pipeline
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultEndpoint), new DefaultAzureCredential());
    logger.LogInformation("Added Azure Key Vault to configuration pipeline.");
}

// Add Application Insights telemetry
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights:InstrumentationKey"]);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddApplicationInsights();

// Add AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add scoped services
builder.Services.AddScoped<ICropRepository, CropRepository>();
builder.Services.AddScoped<IWeatherRepository, WeatherRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IConditionThresholdRepository, ConditionThresholdRepository>();
builder.Services.AddScoped<IGrowthCycleRepository, GrowthCycleRepository>();
builder.Services.AddScoped<IGrowthStageRepository, GrowthStageRepository>();
builder.Services.AddScoped<IHealthScoreRepository, HealthScoreRepository>();
builder.Services.AddScoped<WeatherHealthService>();
builder.Services.AddScoped<HealthEvaluatorService>();
builder.Services.AddTransient<IEmailService, SendGridEmailService>();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure PostgreSQL Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
logger.LogInformation($"Using connection string: {connectionString}");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Apply migrations at startup only in production
if (app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction()) // Enable Swagger in both Development and Production
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
