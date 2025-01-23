using AgriWeatherTracker.Data;

public class TemperatureEvaluator : IWeatherEvaluator
    {
        private readonly AppDbContext _dbContext;

        public TemperatureEvaluator(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public double EvaluateImpact(WeatherDTO weather, ConditionThreshold adverse, ConditionThreshold? optimal, HealthScore? healthScore)
        {
            if (weather.Temperature >= optimal.MinTemperature && weather.Temperature <= optimal.MaxTemperature)
            {
                return -5; // Reward for optimal temperature
            }
            if (weather.Temperature >= adverse.ExtremeMinTemp && weather.Temperature <= adverse.ExtremeMaxTemp)
            {
                return 100.0 / adverse.ExtremeResilienceDuration; // High penalty
            }
            if (weather.Temperature >= adverse.SevereMinTemp && weather.Temperature <= adverse.SevereMaxTemp)
            {
                return 75.0 / adverse.SevereResilienceDuration;
            }
            if (weather.Temperature >= adverse.ModerateMinTemp && weather.Temperature <= adverse.ModerateMaxTemp)
            {
                return 50.0 / adverse.ModerateResilienceDuration;
            }
            if (weather.Temperature >= adverse.MildMinTemp && weather.Temperature <= adverse.MildMaxTemp)
            {
                return 25.0 / adverse.MildResilienceDuration;
            }

            return 0; // No impact if no conditions are met
        }
    }
