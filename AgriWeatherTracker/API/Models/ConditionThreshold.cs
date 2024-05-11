using System;
using System.Collections.Generic;

public class ConditionThreshold
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }

    // Temperature thresholds for different severity levels
    public double MildMinTemp { get; set; }
    public double MildMaxTemp { get; set; }
    public int MildResilienceDuration { get; set; }

    public double ModerateMinTemp { get; set; }
    public double ModerateMaxTemp { get; set; }
    public int ModerateResilienceDuration { get; set; }

    public double SevereMinTemp { get; set; }
    public double SevereMaxTemp { get; set; }
    public int SevereResilienceDuration { get; set; }

    public double ExtremeMinTemp { get; set; }
    public double ExtremeMaxTemp { get; set; }
    public int ExtremeResilienceDuration { get; set; }

    // Other environmental factors
    public double OptimalHumidity { get; set; }
    public double MinHumidity { get; set; }
    public double MaxHumidity { get; set; }
    public double MinRainfall { get; set; }
    public double MaxRainfall { get; set; }
    public double MaxWindSpeed { get; set; }
}
