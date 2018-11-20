using Microsoft.AspNetCore.Http;

namespace Weather.Api.Infrastructure.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static void AddResourceExpiration(this HttpResponse response, string message)
        {
            response.Headers.Add("Expiration", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Expiration");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
        }
    }
}