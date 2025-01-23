public class FungiRiskEvaluator
{
    public static bool IsAtRisk(WeatherDTO weather, ConditionThreshold adverse, HealthScore healthScore)
    {
        return weather.Humidity > adverse.MaxHumidity && healthScore.HighHumidityDuration > adverse.HighHumidityThreshold;
    }
}
