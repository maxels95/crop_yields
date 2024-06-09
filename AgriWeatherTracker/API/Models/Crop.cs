using System;
using System.Collections.Generic;

    public class Crop
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<GrowthCycle> GrowthCycles { get; set; } = new List<GrowthCycle>();
    public List<Location> Locations { get; set; } = new List<Location>(); // Directly linking locations to crops
    public HealthScore HealthScore { get; set; }
}

