using RestSharp;

namespace Aemenersol.Api
{
    internal class AuthEndpoint : BaseApiEndpoint, IAuthEndpoint
    {
        public AuthEndpoint(RestClient client) : base(client)
        {
        }

        public string GetAccessToken(string username, string password)
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{ ""username"": """ + username + @""", ""password"":""" + password + @"""}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            IRestResponse response = RequestClient.Execute(request);

            return response.Content;
        }
    }
}