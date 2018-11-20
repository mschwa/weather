using System;

namespace Weather.Api.Models
{
    public class Climate
    {
        public string Zip { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Temp { get; set; }
        public string MinTemp { get; set; }
        public string MaxTemp { get; set; }
        public string Pressure { get; set; }
        public string Humidity { get; set; }
        public string Elevation { get; set; }
        public string TimeZoneName { get; set; }
        public string Display => ToString();

        public override string ToString()
        {
            return $"Today in '{Location}', the current weather is {Description}. "+ 
                   $"The temperature is {Temp}, with a min temp of {MinTemp} and a max temp of {MaxTemp}. " +
                   $"Air pressure is set to {Pressure} with a humidity of {Humidity}. The timezone is {TimeZoneName}, and the elevation is {Elevation}.";
        }
    }
}