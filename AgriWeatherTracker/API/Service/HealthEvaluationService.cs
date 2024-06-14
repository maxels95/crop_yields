public class HealthEvaluatorService
{
    // This method now accepts an existing HealthScoreDto and returns an updated score.
    public double EvaluateTemperatureImpact(WeatherDTO weather, ConditionThreshold optimal, ConditionThreshold adverse, HealthScore healthScore)
    {
        double score = healthScore.Score; // Start with the existing score.

        // Check each temperature severity level and update the score accordingly.
        if (weather.Temperature >= optimal.MinTemperature && weather.Temperature <= optimal.MaxTemperature)
        {
            score = 0; // Optimal conditions reset the score to 0.
        }
        else if (weather.Temperature >= adverse.MildMinTemp && weather.Temperature <= adverse.MildMaxTemp)
        {
            score += 100.0 / adverse.MildResilienceDuration;
        }
        else if (weather.Temperature >= adverse.ModerateMinTemp && weather.Temperature <= adverse.ModerateMaxTemp)
        {
            score += 100.0 / adverse.ModerateResilienceDuration;
        }
        else if (weather.Temperature >= adverse.SevereMinTemp && weather.Temperature <= adverse.SevereMaxTemp)
        {
            score += 100.0 / adverse.SevereResilienceDuration;
        }
        else if (weather.Temperature >= adverse.ExtremeMinTemp && weather.Temperature <= adverse.ExtremeMaxTemp)
        {
            score += 100.0 / adverse.ExtremeResilienceDuration;
        }

        return score; // Return the modified score.
    }
}
