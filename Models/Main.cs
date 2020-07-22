using System;
using System.Collections.Generic;

#nullable disable

namespace WeatherForecast.Models
{
    public partial class Main
    {
        public int MainId { get; set; }
        public double? Temp { get; set; }
        public double? Feels_Like { get; set; }
        public double? Temp_Min { get; set; }
        public double? Temp_Max { get; set; }
        public int? Pressure { get; set; }
        public int? Humidity { get; set; }
        public int WeatherId { get; set; }

        public virtual Weather Weather { get; set; }
    }
}
