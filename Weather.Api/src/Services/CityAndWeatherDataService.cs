using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Weather.Api.Models;
using Weather.Api.Infrastructure.Configuration;

namespace Weather.Api.Services
{
    public class CityAndWeatherDataService : AbstractResourceRetrievalService<CityAndWeatherData>, IResourceRetrievalService<CityAndWeatherData>
    {
        public CityAndWeatherDataService(IRestClientFactory restFactory, ICachingService cache, ILogger<CityAndWeatherDataService> logger,
            IConfigurationBinder config) : base(restFactory, cache, logger, config) 
        {
        }

        public async Task<CityAndWeatherData> GetResourceAsync(string identifier)
        {
            var parameters = new Dictionary<string, string> { { "zip", identifier } };
            return await GetResourceAsync(identifier, parameters);
        }
    }
}