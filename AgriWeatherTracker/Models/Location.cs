using System;
using System.Collections.Generic;

public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<CropLocation> CropLocations { get; set; } = new List<CropLocation>();
    }