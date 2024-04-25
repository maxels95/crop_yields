using System;
using System.Collections.Generic;

public class ConditionThreshold
    {
        public int Id { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public double OptimalHumidity { get; set; }
        public double MinRainfall { get; set; }
        public double MaxRainfall { get; set; }
    }