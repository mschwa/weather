using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Weather.Api.Infrastructure.Extensions;
using Weather.Api.Models;
using Weather.Api.Services;

namespace Weather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClimateController : ControllerBase
    {
        private const string _unexpectedErrorMessage =
            "We're sorry. The weather api is experiencing technical difficulties. " +
            "Our support team has been notiofied of the problem. Please try again later.";

        private readonly IResourceRetrievalService<CityAndWeatherData> _cityService;
        private readonly IResourceRetrievalService<ElevationData> _elevationService;
        private readonly IResourceRetrievalService<TimeZoneData> _timezoneService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ClimateController(IResourceRetrievalService<CityAndWeatherData> cityService,
            IResourceRetrievalService<TimeZoneData> timezoneService,
            IResourceRetrievalService<ElevationData> elevationService, IMapper mapper,
            ILogger<ClimateController> logger)
        {
            _cityService = cityService;
            _timezoneService = timezoneService;
            _elevationService = elevationService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "GetClimate")]
        public async Task<IActionResult> Get(string id)
        {
            if (!id.IsZipCode())
                return BadRequest("Invalid zipcode. Please check input an try again");

            var builder =
                new ClimateResourceBuilder(_cityService, _timezoneService, _elevationService, _mapper, _logger);

            if (!await builder.BuildCityWeatherDataAsync(id))
            {
                return StatusCode(500, _unexpectedErrorMessage);
            }

            var cityWeatherData = builder.GetClimate();

            if (!await builder.BuildTimeZoneAsync(cityWeatherData.Lat, cityWeatherData.Lon))
            {
                return StatusCode(500, _unexpectedErrorMessage);
            }

            if (!await builder.BuildElevationAsync(cityWeatherData.Lat, cityWeatherData.Lon))
            {
                return StatusCode(500, _unexpectedErrorMessage);
            }

            var climate = builder.GetClimate();

            return CreatedAtRoute("GetClimate", climate);
        }
    }
}