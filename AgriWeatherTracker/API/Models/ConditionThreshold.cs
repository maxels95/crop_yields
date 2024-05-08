using System;
using System.Collections.Generic;

public class ConditionThreshold
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public double OptimalHumidity { get; set; }
    public double MinHumidity { get; set; }
    public double MaxHumidity { get; set; }
    public double MinRainfall { get; set; }
    public double MaxRainfall { get; set; }
    // Adding wind thresholds as an example of other relevant factors
    public double MinWindSpeed { get; set; }
    public double MaxWindSpeed { get; set; }
    // Duration in days that the crop can tolerate adverse conditions without significant damage
    public int ResilienceDuration { get; set; }
}