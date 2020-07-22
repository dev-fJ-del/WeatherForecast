using System;
using System.Collections.Generic;

#nullable disable

namespace WeatherForecast.Models
{
    public partial class Weather
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string City { get; set; }

        public virtual Main Main { get; set; }
    }
}
