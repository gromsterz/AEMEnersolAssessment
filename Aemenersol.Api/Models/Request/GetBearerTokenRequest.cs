using System.ComponentModel.DataAnnotations;

namespace Aemnersol.Api.Models.Request
{
    public class GetBearerTokenRequest
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}