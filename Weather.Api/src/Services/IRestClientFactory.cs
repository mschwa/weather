using RestSharp;

namespace Weather.Api.Services
{
    public interface IRestClientFactory
    {
        IRestClient Create(string baseUrl);
    }
}