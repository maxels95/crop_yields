using System;
using System.Collections.Generic;
using AgriWeatherTracker.Models;

public class CropLocation
    {
        public int CropId { get; set; }
        public Crop Crop { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }