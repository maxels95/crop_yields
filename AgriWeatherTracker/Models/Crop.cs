using System;
using System.Collections.Generic;

namespace AgriWeatherTracker.Models
{
    public class Crop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GrowthCycle> GrowthCycles { get; set; } = new List<GrowthCycle>();
    }
}
