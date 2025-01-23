using AgriWeatherTracker.Models;
using AutoMapper;

namespace AgriWeatherTracker.Service;
public class WeatherHealthService
{
    private readonly IWeatherRepository _weatherRepository;
    private readonly ICropRepository _cropRepository;
    private readonly IHealthScoreRepository _healthScoreRepository;
    private readonly HealthEvaluatorService _healthEvaluatorService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public WeatherHealthService(
        IWeatherRepository weatherRepository,
        ICropRepository cropRepository,
        IHealthScoreRepository healthScoreRepository,
        HealthEvaluatorService healthEvaluatorService,
        IEmailService emailService,
        IMapper mapper)
    {
        _weatherRepository = weatherRepository;
        _cropRepository = cropRepository;
        _healthScoreRepository = healthScoreRepository;
        _healthEvaluatorService = healthEvaluatorService;
        _emailService = emailService;
        _mapper = mapper;
    }

    public async Task<List<string>> UpdateHealthScoresForCrop(int cropId, DateTime startDate, DateTime endDate)
    {
        var returnStrings = new List<string>();
        var crop = await _cropRepository.GetCropByIdAsync(cropId);
        var weathers = new List<Weather>();
        var healtScores = new Dictionary<int, HealthScore>();
        var stages = crop.GrowthCycles.SelectMany(gc => gc.Stages);

        foreach (var location in crop.Locations)
        {
            var weatherData = await _weatherRepository.GetWeatherByLocationAndDateRangeAsync(location.Id, startDate, endDate);
            var healthScore = await _healthScoreRepository.GetHealthScoreByLocationIdAsync(location.Id);
            weathers.AddRange(weatherData);

            if (healthScore == null)
            {
                // Initialize a new health score if none exists
                healthScore = new HealthScore { LocationId = location.Id, Score = 0, Date = DateTime.UtcNow };
                // Consider whether you need to add it to the database here or handle it differently
            }
            healtScores.Add(location.Id, healthScore);
        }

       
        foreach (var weather in weathers.OrderBy(w => w.Date))
        {
            var stage = stages.FirstOrDefault(s => s.StartDate <= weather.Date && s.EndDate >= weather.Date);

            // if (stage != null)
            // {
            //     double newScore = _healthEvaluatorService.EvaluateTemperatureImpact(
            //         _mapper.Map<WeatherDTO>(weather), stage.OptimalConditions, stage.AdverseConditions, healtScores[weather.Location.Id]);
                    
            //     healtScores[weather.Location.Id].Score = newScore;
            //     healtScores[weather.Location.Id].Date = weather.Date;

            //     if (newScore >= 100)
            //     {
            //         returnStrings.Add($"Buy signal generated for {crop.Name}!");
            //         returnStrings.Add($"score: {newScore}");
            //         returnStrings.Add($"Location: {weather.Location.Name} | Date: {weather.Date} | Latest temperature: {weather.Temperature} ");
            //         returnStrings.Add("---------------------------------------------------------------");

            //         string emailBody = $"Alert: Buy signal generated for {crop.Name}!\nScore: {newScore}\nLocation: {weather.Location.Name} | Date: {weather.Date} | Temperature: {weather.Temperature}";
            //         await _emailService.SendEmailAsync("your-email@example.com", "Health Score Alert", emailBody, null, null);
            //     }
            // }
            

            // healthScore.Score = newScore;
            // if (healthScore.Id == 0)
            // {
            //     await _healthScoreRepository.CreateHealthScoreAsync(_mapper.Map<HealthScore>(healthScore));
            // }
            // else
            // {
            //     await _healthScoreRepository.UpdateHealthScoreAsync(_mapper.Map<HealthScore>(healthScore));
            // }
        }
        return(returnStrings);
    }
}
