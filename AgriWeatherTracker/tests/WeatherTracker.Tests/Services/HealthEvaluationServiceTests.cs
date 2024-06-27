using Xunit;
using Moq;
using AgriWeatherTracker.Models;  // Import your models
using AgriWeatherTracker.Service; // Import services if they are in a specific namespace

public class HealthEvaluatorServiceTests
{
    private readonly HealthEvaluatorService _service;

    public HealthEvaluatorServiceTests()
    {
        _service = new HealthEvaluatorService();
    }

    [Fact]
    public void EvaluateTemperatureImpact_ReturnsZero_WhenTemperatureIsOptimal()
    {
        // Arrange
        var weather = new WeatherDTO { Temperature = 25 };
        var optimal = new ConditionThreshold { MinTemperature = 20, MaxTemperature = 30 };
        var adverse = new ConditionThreshold();
        var healthScore = new HealthScore { Score = 50 };

        // Act
        var result = _service.EvaluateTemperatureImpact(weather, optimal, adverse, healthScore);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void EvaluateTemperatureImpact_IncrementsScore_WhenTemperatureIsAdverse()
    {
        // Arrange
        var weather = new WeatherDTO { Temperature = 35 };
        var optimal = new ConditionThreshold { MinTemperature = 20, MaxTemperature = 30 };
        var adverse = new ConditionThreshold { ExtremeMinTemp = 34, ExtremeMaxTemp = 40, ExtremeResilienceDuration = 2 };
        var healthScore = new HealthScore { Score = 50 };

        // Act
        var result = _service.EvaluateTemperatureImpact(weather, optimal, adverse, healthScore);

        // Assert
        Assert.Equal(100, result); // Check if the score has been correctly updated
    }
}
