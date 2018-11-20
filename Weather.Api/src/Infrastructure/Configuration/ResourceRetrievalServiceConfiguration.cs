namespace Weather.Api.Infrastructure.Configuration
{
    public class ResourceRetrievalServiceConfiguration
    {
        public string BaseUrl { get; set; }
        public string ApiKeyName { get; set; }
        public string ApiKeyValue { get; set; }
        public string ResourceName { get; set; }
        public bool UseCache { get; set; }
        public string CacheKey { get; set; }
        public int CacheExpirationInMinutes { get; set; }
    }
}