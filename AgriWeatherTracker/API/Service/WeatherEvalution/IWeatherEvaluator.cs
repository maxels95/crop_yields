public interface IWeatherEvaluator
{
    double EvaluateImpact(WeatherDTO weather, ConditionThreshold adverse, ConditionThreshold? optimal, HealthScore? healthScore);
}
