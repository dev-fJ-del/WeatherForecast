using System;
using System.Collections.Generic;

#nullable disable

namespace WeatherForecast.Models
{
    public partial class UserWeather
    {
        public Guid? UserId { get; set; }
        public int? WeatherId { get; set; }
        public int UserWeatherId { get; set; }
    }
}
