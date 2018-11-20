using System;
using RestSharp;

namespace Weather.Api.Services
{
    public class RetrievalRequestException<T> : Exception where T : class
    {
        public IRestResponse<T> Response { get; set; }

        public RetrievalRequestException(IRestResponse<T> response, string error) : base(error)
        {
            Response = response;
        }
    }
}
