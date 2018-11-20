using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Weather.Api.Infrastructure.Extensions;
using Weather.Api.Models;

namespace Weather.Api.Services
{
    public class ClimateResourceBuilder
    {
        public const string _coordsError =
            "Builder instance has called 'BuildTimeZone' before the coordinates have been set. " +
            "Either call 'BuildCityWeatherData' first, or check the prevous error logs to see if that call has failed";

        private readonly IResourceRetrievalService<CityAndWeatherData> _cityService;
        private readonly IResourceRetrievalService<TimeZoneData> _timezoneService;
        private readonly IResourceRetrievalService<ElevationData> _elevationService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        private Climate _climate;

        public ClimateResourceBuilder(IResourceRetrievalService<CityAndWeatherData> cityService,
            IResourceRetrievalService<TimeZoneData> timezoneService,
            IResourceRetrievalService<ElevationData> elevationService, IMapper mapper, ILogger logger)
        {
            _cityService = cityService;
            _timezoneService = timezoneService;
            _elevationService = elevationService;
            _mapper = mapper;
            _logger = logger;
            _climate = new Climate();
        }

        public async Task<bool> BuildCityWeatherDataAsync(string zipcode)
        {
            try
            {
                var data = await _cityService.GetResourceAsync(zipcode);
                _climate = _mapper.Map(data, _climate);

                _climate.Zip = zipcode;
            }
            catch (RetrievalRequestException<CityAndWeatherData>)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> BuildTimeZoneAsync(string lat, string lon)
        {
            if (lat.IsBlank() || lon.IsBlank())
                _logger.LogError(_coordsError);

            try
            {
                var data = await _timezoneService.GetResourceAsync($"{_climate.Lat},{_climate.Lon}");
                _mapper.Map(data, _climate);
            }
            catch (RetrievalRequestException<TimeZoneData>)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> BuildElevationAsync(string lat, string lon)
        {
            if (lat.IsBlank() || lon.IsBlank())
                _logger.LogError(_coordsError);

            try
            {
                var data = await _elevationService.GetResourceAsync($"{lat},{lon}");
                _mapper.Map(data, _climate);
            }
            catch (RetrievalRequestException<ElevationData>)
            {
                return false;
            }

            return true;
        }

        public Climate GetClimate()
        {
            return _climate;
        }
    }
}