using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Weather.Api.Infrastructure.Configuration;
using Weather.Api.Models;

namespace Weather.Api.Services
{
    public class TimeZoneDataService : AbstractResourceRetrievalService<TimeZoneData>, IResourceRetrievalService<TimeZoneData>
    {
        public TimeZoneDataService(IRestClientFactory restFactory, ICachingService cache, ILogger<TimeZoneDataService> logger,
            IConfigurationBinder config) : base(restFactory, cache, logger, config)
        {
        }

        public async Task<TimeZoneData> GetResourceAsync(string identifier)
        {
            var parameters = new Dictionary<string, string>
            {
                { "location", identifier },
                { "timestamp", GenerateGoogleTimeStamp() }
            };

            return await GetResourceAsync(identifier, parameters);
        }
    }
}