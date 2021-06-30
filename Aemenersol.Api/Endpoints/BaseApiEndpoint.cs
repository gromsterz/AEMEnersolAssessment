using RestSharp;

namespace Aemenersol.Api
{
    internal class BaseApiEndpoint
    {
        private readonly string AccessToken;
        protected readonly RestClient RequestClient;

        public BaseApiEndpoint(RestClient client = null, string accessToken = null)
        {
            RequestClient = client;
            AccessToken = accessToken;
        }
    }
}