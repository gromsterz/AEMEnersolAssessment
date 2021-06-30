using Aemenersol.Api.Models;
using RestSharp;

namespace Aemenersol.Api
{
    public static class AemenersolApi
    {
        private static ApiSettings ApiSettings;
        private static string BearerToken;

        public static void Setup(ApiSettings apiSettings)
        {
            ApiSettings = apiSettings;
        }

        public static IAuthEndpoint GetAuthEndpoint()
        {
            return new AuthEndpoint(GetRestClient("/Account/Login"));
        }

        public static IPlatformWellEndpoint GetPlatformWellEndpoint()
        {
            return new PlatformWellEndpoint();
        }

        public static void SetBearerToken(string bearerToken)
        {
            // BearerToken provided by the api include the " which should be discarded
            BearerToken = bearerToken.Replace("\"", "");
        }

        internal static string GetBearerToken()
        {
            return BearerToken;
        }

        internal static RestClient GetRestClient(string path)
        {
            var client = new RestClient($"{ApiSettings.Host}{path}");
            client.Timeout = -1;
            return client;
        }
    }
}