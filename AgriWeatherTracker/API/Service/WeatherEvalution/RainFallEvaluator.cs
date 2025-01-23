public class RainfallEvaluator : IWeatherEvaluator
{
    public double EvaluateImpact(WeatherDTO weather, ConditionThreshold adverse, ConditionThreshold? optimal, HealthScore? healthScore)
    {
        // Add daily rainfall to cumulative total
        healthScore.CumulativeRainfall += weather.Rainfall;

        // Define weekly thresholds (example values, adjust as needed)
        double weeklyMinRainfall = adverse.MinRainfall * 7; // Minimum total rainfall over 7 days
        double weeklyMaxRainfall = adverse.MaxRainfall * 7; // Maximum total rainfall over 7 days

        // Evaluate cumulative rainfall impact
        double impact = 0;
        if (healthScore.CumulativeRainfall < weeklyMinRainfall)
        {
            impact = (weeklyMinRainfall - healthScore.CumulativeRainfall) * 0.5; // Drought penalty, scaled
        }
        else if (healthScore.CumulativeRainfall > weeklyMaxRainfall)
        {
            impact = (healthScore.CumulativeRainfall - weeklyMaxRainfall) * 0.5; // Overwatering penalty, scaled
        }

        // Reset cumulative rainfall every 7 days
        if (weather.Date.DayOfWeek == DayOfWeek.Sunday)
        {
            healthScore.CumulativeRainfall = 0;
        }

        return impact;
    }
}
