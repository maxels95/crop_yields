using System;

namespace AgriWeatherTracker.Models
{
    public class Weather
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double Rainfall { get; set; }
        public double WindSpeed { get; set; }
        public Location Location { get; set; }
    }
}
