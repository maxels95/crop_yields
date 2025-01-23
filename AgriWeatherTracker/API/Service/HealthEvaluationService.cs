using AgriWeatherTracker.Data;

namespace AgriWeatherTracker.Service
{
    public class HealthEvaluatorService
    {
        private readonly IEnumerable<IWeatherEvaluator> _evaluators;
        private readonly AppDbContext _dbContext;

        public HealthEvaluatorService(IEnumerable<IWeatherEvaluator> evaluators, AppDbContext dbContext)
        {
            _evaluators = evaluators;
            _dbContext = dbContext;
        }

        public HealthScore EvaluateWeatherImpact(
            WeatherDTO weather,
            ConditionThreshold optimal,
            ConditionThreshold adverse,
            HealthScore healthScore)
        {
            double positiveImpact = 0;
            double negativeImpact = 0;

            foreach (var evaluator in _evaluators)
            {
                var impact = evaluator.EvaluateImpact(weather, optimal, adverse, healthScore);

                // Adjust impacts dynamically based on context
                if (evaluator is HumidityEvaluator && weather.Rainfall > optimal.MinRainfall)
                {
                    impact *= 0.5; // Reduce humidity penalty if there's sufficient rainfall
                }

                if (impact > 0)
                    negativeImpact += impact;
                else
                    positiveImpact += Math.Abs(impact);
            }

            // Calculate combined resilience and score
            healthScore.Resilience -= negativeImpact;
            healthScore.Resilience = Math.Max(0, healthScore.Resilience);

            healthScore.Score = Math.Max(0, healthScore.Score + positiveImpact - negativeImpact);

            // Update risk flags
            healthScore.IsAtRiskOfFungi = CheckFungiRisk(weather, adverse);
            healthScore.IsAtRiskOfCropLoss = healthScore.Resilience == 0;

            // Persist updated HealthScore
            _dbContext.HealthScores.Update(healthScore);
            _dbContext.SaveChanges();

            return healthScore;
        }

        private bool CheckFungiRisk(WeatherDTO weather, ConditionThreshold adverse)
        {
            return weather.Humidity > adverse.MaxHumidity;
        }
    }
}
