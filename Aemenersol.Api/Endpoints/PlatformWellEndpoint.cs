using Aemenersol.Entity;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;

namespace Aemenersol.Api
{
    internal class PlatformWellEndpoint : BaseApiEndpoint, IPlatformWellEndpoint
    {
        public PlatformWellEndpoint(RestClient client = null) : base(client)
        {
        }

        public List<Platform> GetPlatformWells(bool actualData = true)
        {
            // Depending the structure and protocol provided by the API Endpoint this checking should not be necessary.
            // Standard pactise the same entity/object should only have one endpoint with different protocol for different operations which allow us to use only RestClient provided when initiating this object.
            RestClient client;
            if (actualData)
                client = AemenersolApi.GetRestClient("/PlatformWell/GetPlatformWellActual");
            else
                client = AemenersolApi.GetRestClient("/PlatformWell/GetPlatformWellDummy");

            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "Bearer " + AemenersolApi.GetBearerToken());

            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<Platform>>(response.Content);
        }
    }
}