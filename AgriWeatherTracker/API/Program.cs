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

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Enable legacy timestamp behavior for Npgsql
    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    Console.WriteLine("Enabled legacy timestamp behavior for Npgsql.");

    // Add initial console logging to capture early errors
    Console.WriteLine("Configuring logging...");

    // Add logging to verify initial configuration
    var loggerFactory = LoggerFactory.Create(builder => 
    {
        builder.AddConsole();
        builder.AddDebug();
    });
    var logger = loggerFactory.CreateLogger<Program>();

    // Log initial configuration
    Console.WriteLine("Starting application configuration...");
    logger.LogInformation("Starting application configuration...");

    // Add services to the container.
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
    logger.LogInformation("Added controllers with JSON options.");

    // Retrieve Key Vault Endpoint from app settings
    var keyVaultEndpoint = builder.Configuration["KeyVault:VaultUri"];
    Console.WriteLine($"Key Vault Endpoint: {keyVaultEndpoint}");
    logger.LogInformation($"Key Vault Endpoint: {keyVaultEndpoint}");

    if (!string.IsNullOrEmpty(keyVaultEndpoint))
    {
        try
        {
            // Add Azure Key Vault to the configuration pipeline
            builder.Configuration.AddAzureKeyVault(new Uri(keyVaultEndpoint), new DefaultAzureCredential());
            Console.WriteLine("Added Azure Key Vault to configuration pipeline.");
            logger.LogInformation("Added Azure Key Vault to configuration pipeline.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during Key Vault setup: {ex}");
            logger.LogError(ex, "Error adding Azure Key Vault to configuration pipeline.");
            throw;
        }
    }
    else
    {
        Console.WriteLine("Key Vault Endpoint is not configured.");
        logger.LogWarning("Key Vault Endpoint is not configured.");
    }

    // Add Application Insights telemetry
    builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights:InstrumentationKey"]);
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.AddApplicationInsights();
    logger.LogInformation("Configured Application Insights telemetry and logging.");

    // Add AutoMapper
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    logger.LogInformation("Added AutoMapper.");

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
    logger.LogInformation("Added scoped services.");

    // Configure Swagger/OpenAPI
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    logger.LogInformation("Configured Swagger/OpenAPI.");

    // Configure PostgreSQL Database
    try
    {
        // Retrieve connection string
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
        }

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string 'DefaultConnection' is not configured.");
        }

        // Sanitize the connection string for logging (avoid logging sensitive information)
        var sanitizedConnectionString = connectionString
            .Replace("Password=", "Password=******")
            .Replace("username=", "username=******");

        Console.WriteLine($"Retrieved connection string: {sanitizedConnectionString}");
        logger.LogInformation($"Retrieved connection string: {sanitizedConnectionString}");

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));
        logger.LogInformation("Configured PostgreSQL Database.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception during DB context setup: {ex}");
        logger.LogError(ex, "Error configuring the database context.");
        throw;
    }

    var app = builder.Build();
    logger.LogInformation("Application built.");

    // Apply migrations at startup only in production
    if (app.Environment.IsProduction())
    {
        try
        {
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
                logger.LogInformation("Database migrations applied successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception during database migration: {ex}");
            logger.LogError(ex, "Error applying database migrations.");
            throw;
        }

        // Set the port to 8080 in production
        var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
        app.Urls.Add($"http://*:{port}");
        Console.WriteLine($"Running on port {port}");
        logger.LogInformation($"Running on port {port}");
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

    logger.LogInformation("Application starting up...");
    app.Run();
    logger.LogInformation("Application has started.");
}
catch (Exception ex)
{
    // Capture any startup exceptions
    Console.WriteLine($"Application startup exception: {ex}");
    throw;
}
