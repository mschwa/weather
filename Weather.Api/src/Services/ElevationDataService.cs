using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Weather.Api.Infrastructure.Configuration;
using Weather.Api.Models;

namespace Weather.Api.Services
{
    public class ElevationDataService : AbstractResourceRetrievalService<ElevationData>, IResourceRetrievalService<ElevationData>
    {
        public ElevationDataService(IRestClientFactory restFactory, ICachingService cache, ILogger<ElevationDataService> logger,
            IConfigurationBinder config) : base(restFactory, cache, logger, config)
        {
        }

        public async Task<ElevationData> GetResourceAsync(string identifier)
        {
            var parameters = new Dictionary<string, string>
            {
                { "locations", identifier },
                { "timestamp", GenerateGoogleTimeStamp() }
            };

            return await GetResourceAsync(identifier, parameters);
        }
    }
}