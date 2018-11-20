using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moq;
using RestSharp;
using Weather.Api.Infrastructure.Configuration;
using Weather.Api.Models;
using Weather.Api.Services;

namespace Weather.Api.Tests.Service
{
    public class BaseServiceTest<T>
    {
        protected Mock<ILogger<T>> _logger;
        protected Mock<IConfigurationBinder> _configBinder;
        protected Mock<ICachingService> _memCache;
        protected Mock<IRestClientFactory> _restClientFactory;
        protected Mock<IRestClient> _restClient;

        protected BaseServiceTest()
        {
            _logger = new Mock<ILogger<T>>();
            _configBinder = new Mock<IConfigurationBinder>();
            _memCache = new Mock<ICachingService>();
            _restClientFactory = new Mock<IRestClientFactory>();
            _restClient = new Mock<IRestClient>();

            _restClientFactory.Setup(c => c.Create(It.IsAny<string>())).Returns(_restClient.Object);

            _configBinder.Setup(c => c.GetConfiguration(It.IsAny<string>())).Returns(
                new ResourceRetrievalServiceConfiguration
                {
                    BaseUrl = "http://www.test.com",
                    ApiKeyName = "key",
                    CacheExpirationInMinutes = 1,
                    CacheKey = "test",
                    ApiKeyValue = "test",
                    ResourceName = "resource",
                    UseCache = true
                });
        }

        internal static Climate NewClimate()
        {
            return new Climate
            {
                TimeZoneName = "Test",
                Description = "Test",
                Elevation = "123",
                Humidity = "123",
                Temp = "123",
                Lat = "123",
                Lon = "123",
                Pressure = "123",
                Location = "123",
                MaxTemp = "123",
                MinTemp = "123",
                Zip = "123"
            };
        }

        internal static CityAndWeatherData NewCityAndWeatherData()
        {
            var description = new CityAndWeatherDescription { Description = "cloudy", Main = "cloudy" };

            var data = new CityAndWeatherData
            {
                Code = "200",
                Coord = new Coordinates { Lat = "-122.354", Lon = "45.3455" },
                Main = new WeatherStatistics
                    { Humidity = "53454", Pressure = "52345", Temp = "74567", Temp_Max = "73567", Temp_Min = "763567" },
                Name = "Portland, OR",
                Weather = new List<CityAndWeatherDescription>(new List<CityAndWeatherDescription>(new[] { description }))
            };

            return data;
        }

        internal static TimeZoneData NewTimeZoneData()
        {
            return new TimeZoneData { TimeZoneId = "Pacific", TimeZoneName = "Pacific" };
        }

        internal static ElevationData NewElevationData()
        {
            return new ElevationData { Elevation = "13456" };
        }
    }
}
