using System.Collections.Generic;
using System.Security;

namespace Weather.Api.Models
{
    public class CityAndWeatherData
    {
        public string Name { get; set; }
        public List<CityAndWeatherDescription> Weather { get; set; }
        public Coordinates Coord { get; set; }
        public WeatherStatistics Main { get; set; }
        public string Code { get; set; }
    }
}