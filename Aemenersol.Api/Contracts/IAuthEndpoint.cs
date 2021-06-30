namespace Aemenersol.Api
{
    public interface IAuthEndpoint
    {
        public string GetAccessToken(string username, string password);
    }
}