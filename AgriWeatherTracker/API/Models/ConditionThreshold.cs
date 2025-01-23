using System;
using System.Collections.Generic;

public class ConditionThreshold
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Temperature thresholds for different severity levels
    public double MinTemperature { get; set; }
    public double MaxTemperature { get; set; }
    public double MildMinTemp { get; set; }
    public double MildMaxTemp { get; set; }
    public int MildResilienceDuration { get; set; } // Days of exposure
    public double ModerateMinTemp { get; set; }
    public double ModerateMaxTemp { get; set; }
    public int ModerateResilienceDuration { get; set; }
    public double SevereMinTemp { get; set; }
    public double SevereMaxTemp { get; set; }
    public int SevereResilienceDuration { get; set; }
    public double ExtremeMinTemp { get; set; }
    public double ExtremeMaxTemp { get; set; }
    public int ExtremeResilienceDuration { get; set; }

    // Humidity thresholds
    public double OptimalHumidity { get; set; } // Ideal humidity range
    public double MinHumidity { get; set; }
    public double MaxHumidity { get; set; }
    public int HumidityResilienceDuration { get; set; } // Days before adverse effects occur

    // Rainfall thresholds
    public double MinRainfall { get; set; } // Minimum for growth
    public double MaxRainfall { get; set; } // Maximum tolerable
    public int RainfallResilienceDuration { get; set; } // Days before waterlogging/rot risk

    // Wind speed thresholds
    public double MaxWindSpeed { get; set; } // Maximum tolerable wind speed
    public int WindResilienceDuration { get; set; } // Days before wind damage accumulates

    // Resilience to cumulative adverse conditions
    public int CumulativeAdverseDaysThreshold { get; set; } // Total adverse days before crop failure
    public int RecoveryDaysThreshold { get; set; } // Days needed for recovery after adverse conditions

    // Crop-specific fungal risk thresholds
    public double HighHumidityThreshold { get; set; } // Humidity level triggering fungal risk
    public int HighHumidityRiskDuration { get; set; } // Days of high humidity before fungal outbreak

    // Additional notes for crop lifecycle
    public string CropLifecycleStage { get; set; } // Flowering, Pod Development, etc.
}
