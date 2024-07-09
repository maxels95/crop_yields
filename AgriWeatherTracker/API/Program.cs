using AgriWeatherTracker.Data;
using AgriWeatherTracker.Service;
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Enable legacy timestamp behavior for Npgsql
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Console.WriteLine("Enabled legacy timestamp behavior for Npgsql.");

        // Configure logging
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        
        var loggerFactory = builder.Logging.Services.BuildServiceProvider().GetService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger<Program>();

        // Add AWS service clients to the container
        builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
        builder.Services.AddAWSService<IAmazonSecretsManager>();

        // Add AWS Secrets Manager to the configuration pipeline
        var secretsManagerClient = builder.Services.BuildServiceProvider().GetService<IAmazonSecretsManager>();
        var secretNames = builder.Configuration.GetSection("AWS:SecretsManager:SecretNames").Get<List<string>>();

        if (secretNames != null && secretNames.Count > 0)
        {
            try
            {
                foreach (var secretName in secretNames)
                {
                    var secretValue = await GetSecretValueAsync(secretsManagerClient, secretName);
                    var secretData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(secretValue);

                    foreach (var secret in secretData)
                    {
                        builder.Configuration[secret.Key] = secret.Value;
                    }
                }

                Console.WriteLine("Added AWS Secrets Manager secrets to configuration pipeline.");
                logger.LogInformation("Added AWS Secrets Manager secrets to configuration pipeline.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during AWS Secrets Manager setup: {ex}");
                logger.LogError(ex, "Error adding AWS Secrets Manager to configuration pipeline.");
                throw;
            }
        }
        else
        {
            Console.WriteLine("AWS Secrets Manager secret names are not configured.");
            logger.LogWarning("AWS Secrets Manager secret names are not configured.");
        }

        // Add services to the container
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        });

        // Add Application Insights telemetry
        builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights:InstrumentationKey"]);

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

    private static async Task<string> GetSecretValueAsync(IAmazonSecretsManager client, string secretName)
    {
        var request = new GetSecretValueRequest
        {
            SecretId = secretName
        };

        var response = await client.GetSecretValueAsync(request);

        if (response.SecretString != null)
        {
            return response.SecretString;
        }
        else
        {
            throw new InvalidOperationException("Secret value is not a string");
        }
    }
}
