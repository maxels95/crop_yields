public class HealthEvaluatorService
{
    public void EvaluateTemperatureImpact(WeatherDTO weather, ConditionThresholdDTO threshold, HealthScoreDto healthScore)
    {
        // Reset score to 0 if within optimal temperature range
        if (weather.Temperature >= threshold.MinTemperature && weather.Temperature <= threshold.MaxTemperature)
        {
            healthScore.Score = 0;
        }
        else
        {
            // Check each temperature severity level and update the health score accordingly
            if (weather.Temperature >= threshold.MildMinTemp && weather.Temperature <= threshold.MildMaxTemp)
            {
                healthScore.Score += 100.0 / threshold.MildResilienceDuration;
            }
            else if (weather.Temperature >= threshold.ModerateMinTemp && weather.Temperature <= threshold.ModerateMaxTemp)
            {
                healthScore.Score += 100.0 / threshold.ModerateResilienceDuration;
            }
            else if (weather.Temperature >= threshold.SevereMinTemp && weather.Temperature <= threshold.SevereMaxTemp)
            {
                healthScore.Score += 100.0 / threshold.SevereResilienceDuration;
            }
            else if (weather.Temperature >= threshold.ExtremeMinTemp && weather.Temperature <= threshold.ExtremeMaxTemp)
            {
                healthScore.Score += 100.0 / threshold.ExtremeResilienceDuration;
            }
        }
    }
}
