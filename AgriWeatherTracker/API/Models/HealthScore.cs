public class HealthScore
{
    public int Id { get; set; } // Unique identifier
    public int CropId { get; set; } // ID of the crop type
    public Crop Crop { get; set; } // Crop entity
    public int LocationId { get; set; } // ID of the location
    public Location Location { get; set; } // Location entity
    public DateTime Date { get; set; } // Date of the health score calculation
    public double Score { get; set; } // Current health score for the crop

    // New fields for additional tracking
    public double Resilience { get; set; } // Tracks how much resilience is left for the crop
    public double PositiveImpact { get; set; } // Tracks the cumulative positive impacts (e.g., optimal weather conditions)
    public double NegativeImpact { get; set; } // Tracks cumulative negative impacts (e.g., severe weather conditions)

    // Breakdown of factors contributing to the score
    public double TemperatureImpact { get; set; } // Specific impact of temperature
    public double HumidityImpact { get; set; } // Specific impact of humidity
    public double RainfallImpact { get; set; } // Specific impact of rainfall
    public double WindImpact { get; set; } // Specific impact of wind

    // Optional: Track warnings for critical conditions
    public int HighHumidityDuration { get; set; } // Tracks consecutive days of high humidity
    public bool IsAtRiskOfFungi { get; set; } // Flag if conditions for fungi outbreak are met
    public bool IsAtRiskOfCropLoss { get; set; } // Flag if critical risk thresholds are reached
    public double CumulativeRainfall { get; set; } = 0;
}

