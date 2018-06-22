using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    class Forecast
    {
        public string Location { get; set; } = "";
        public string Time { get; set; } = "";
        public string Description { get; set; } = "";
        public string Temperature { get; set; } = "";

    }
}
