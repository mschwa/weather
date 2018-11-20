using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RestSharp;
using Weather.Api.Infrastructure.Configuration;

namespace Weather.Api.Services
{
    public abstract class AbstractResourceRetrievalService<T> where T : class
    {
        private const string _error = "We're sorry, our API is experiencing technical difficulties. " +
                                      "Our technical staff has been notified. Please try again later";

        private readonly ResourceRetrievalServiceConfiguration _config;
        private readonly ILogger _logger;
        private readonly IRestClientFactory _restClientFactory;
        private readonly ICachingService _cachingService;

        protected AbstractResourceRetrievalService(IRestClientFactory restClientFactory, ICachingService cachingService,
            ILogger<AbstractResourceRetrievalService<T>> logger, IConfigurationBinder configBinder)
        {
            _config = configBinder.GetConfiguration(typeof(T).Name);

            _restClientFactory = restClientFactory;
            _cachingService = cachingService;
            _logger = logger;
        }

        protected async Task<T> GetResourceAsync(string identifier, IDictionary<string, string> parameters)
        {
            if (_config.UseCache && _cachingService.TryGetValue<T>($"{identifier}-{_config.CacheKey}", out T cached))
                return cached;

            var request = new RestRequest
            {
                Resource = _config.ResourceName,
                Parameters = {new Parameter(_config.ApiKeyName, _config.ApiKeyValue, ParameterType.QueryString)}
            };
            
            foreach (var keyValuePair in parameters) request.AddQueryParameter(keyValuePair.Key, keyValuePair.Value);

            var client = _restClientFactory.Create(_config.BaseUrl);
            var response = await client.ExecuteGetTaskAsync<T>(request);


            // For all intents and purposes we should assume these requests never fail.
            // If they do it's assumed an exceptional case.
            if (!response.IsSuccessful)
            {
                _logger.LogDebug("Data service failed. " + response.Content);
                throw new RetrievalRequestException<T>(response, _error);
            }

            if (_config.UseCache)
                _cachingService
                    .Set($"{identifier}-{_config.CacheKey}", response.Data,
                        DateTimeOffset.Now.AddMinutes(_config.CacheExpirationInMinutes));
            
            return response.Data;
        }

        protected string GenerateGoogleTimeStamp()
        {
            var now = DateTime.UtcNow;
            var then = DateTime.Parse("1/1/1970").ToUniversalTime();

            return Math.Round((now - then).TotalSeconds).ToString(CultureInfo.InvariantCulture);
        }
    }
}