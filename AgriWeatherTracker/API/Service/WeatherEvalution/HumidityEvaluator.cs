using AgriWeatherTracker.Data;

public class HumidityEvaluator : IWeatherEvaluator
{
    private readonly AppDbContext _dbContext;

    public HumidityEvaluator(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public double EvaluateImpact(WeatherDTO weather, ConditionThreshold adverse, ConditionThreshold? optimal, HealthScore? healthScore)
    {
        double penalty = 0;

        if (weather.Humidity >= adverse.MinHumidity && weather.Humidity <= adverse.MaxHumidity)
        {
            // Reset duration and gradually reduce penalty
            if (healthScore.HighHumidityDuration > 0)
            {
                healthScore.HighHumidityDuration = 0;
                penalty -= healthScore.Score * 0.1; // Gradual recovery
            }
        }
        else if (weather.Humidity < adverse.MinHumidity || weather.Humidity > adverse.MaxHumidity)
        {
            // Increment duration for adverse humidity
            healthScore.HighHumidityDuration++;

            // Proportional penalty based on duration
            penalty += (healthScore.HighHumidityDuration / adverse.HighHumidityThreshold) * 20;

            // Cap penalty to avoid runaway growth
            if (penalty > 100) penalty = 100;
        }

        // Update health score and save to DB
        healthScore.Score += penalty;
        _dbContext.HealthScores.Update(healthScore);
        _dbContext.SaveChanges();

        return penalty;
    }
}
