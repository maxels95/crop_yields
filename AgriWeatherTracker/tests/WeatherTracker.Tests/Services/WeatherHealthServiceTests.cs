using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AgriWeatherTracker.Models;
using AgriWeatherTracker.Service;
using AgriWeatherTracker.Mapper;

public class WeatherHealthServiceTests
{
    private readonly WeatherHealthService _weatherHealthService;
    private readonly Mock<IWeatherRepository> _mockWeatherRepository;
    private readonly Mock<ICropRepository> _mockCropRepository;
    private readonly Mock<IHealthScoreRepository> _mockHealthScoreRepository;
    private readonly Mock<IEmailService> _mockEmailService;
    private readonly IMapper _mapper;

    public WeatherHealthServiceTests()
    {
        _mockWeatherRepository = new Mock<IWeatherRepository>();
        _mockCropRepository = new Mock<ICropRepository>();
        _mockHealthScoreRepository = new Mock<IHealthScoreRepository>();
        _mockEmailService = new Mock<IEmailService>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile()); // Use the shared MappingProfile
        });
        _mapper = config.CreateMapper();

        _weatherHealthService = new WeatherHealthService(
            _mockWeatherRepository.Object,
            _mockCropRepository.Object,
            _mockHealthScoreRepository.Object,
            new HealthEvaluatorService(),
            _mockEmailService.Object,
            _mapper
        );
    }

    [Fact]
    public async Task UpdateHealthScoresForCrop_SendsEmail_WhenScoreExceedsThreshold()
    {
        // Arrange
        int cropId = 1;
        DateTime startDate = DateTime.UtcNow.AddDays(-10);
        DateTime endDate = DateTime.UtcNow;

        var crop = new Crop
        {
            Id = cropId,
            Name = "Test Crop",
            Locations = new List<Location> { new Location { Id = 1, Name = "Location 1" } },
            GrowthCycles = new List<GrowthCycle>
            {
                new GrowthCycle
                {
                    Stages = new List<GrowthStage>
                    {
                        new GrowthStage
                        {
                            StartDate = DateTime.UtcNow.AddDays(-10),
                            EndDate = DateTime.UtcNow.AddDays(10),
                            OptimalConditions = new ConditionThreshold(),
                            AdverseConditions = new ConditionThreshold
                            {
                                ExtremeResilienceDuration = 1,
                                SevereResilienceDuration = 2,
                                ModerateResilienceDuration = 3,
                                MildResilienceDuration = 4,
                                ExtremeMinTemp = -10,
                                ExtremeMaxTemp = 50,
                                SevereMinTemp = -5,
                                SevereMaxTemp = 40,
                                ModerateMinTemp = 0,
                                ModerateMaxTemp = 35,
                                MildMinTemp = 5,
                                MildMaxTemp = 30
                            }
                        }
                    }
                }
            }
        };

        _mockCropRepository.Setup(repo => repo.GetCropByIdAsync(cropId)).ReturnsAsync(crop);
        _mockWeatherRepository.Setup(repo => repo.GetWeatherByLocationAndDateRangeAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(new List<Weather>
        {
            new Weather { Date = DateTime.UtcNow, Location = crop.Locations[0], Temperature = 45 }
        });
        _mockHealthScoreRepository.Setup(repo => repo.GetHealthScoreByLocationIdAsync(It.IsAny<int>())).ReturnsAsync((HealthScore)null);

        // Act
        var result = await _weatherHealthService.UpdateHealthScoresForCrop(cropId, startDate, endDate);

        // Assert
        Assert.Contains("Buy signal generated for Test Crop!", result);
        _mockEmailService.Verify(service => service.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), null, null), Times.Once);
    }
}
