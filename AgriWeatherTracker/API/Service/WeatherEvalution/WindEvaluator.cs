public class WindEvaluator : IWeatherEvaluator
{
    public double EvaluateImpact(WeatherDTO weather, ConditionThreshold adverse, ConditionThreshold? optimal, HealthScore? healthScore)
    {
        if (weather.WindSpeed > adverse.MaxWindSpeed)
        {
            throw new CropLossException($"Crop loss due to extreme wind conditions: {weather.WindSpeed} km/h");
        }

        return healthScore?.Score ?? 0;
    }
}
