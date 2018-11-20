using System;
using RestSharp;

namespace Weather.Api.Services
{
    public class RestClientFactory : IRestClientFactory
    {
        public IRestClient Create(string baseUrl)
        {
            return new RestClient { BaseUrl = new Uri(baseUrl) };
        }
    }
}
