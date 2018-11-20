using Microsoft.Extensions.Configuration;

namespace Weather.Api.Infrastructure.Configuration
{
    public interface IConfigurationBinder
    {
        ResourceRetrievalServiceConfiguration GetConfiguration(string serviceTypeName);
    }

    public class ConfigurationBinder : IConfigurationBinder
    {
        private readonly IConfiguration _configuration;

        public ConfigurationBinder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ResourceRetrievalServiceConfiguration GetConfiguration(string serviceTypeName)
        {
            var config = new ResourceRetrievalServiceConfiguration();
            _configuration.GetSection($"ServiceConfigurations:{serviceTypeName}").Bind(config);

            return config;
        }
    }
}