using Aemenersol.Api;
using Aemenersol.Data.DataContext;
using Aemenersol.Data.Synchronizer;
using Aemnersol.Api.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aemnersol.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SyncController : ControllerBase
    {
        private readonly ILogger<SyncController> Logger;
        private readonly ApplicationDbContext DbContext;

        public SyncController(ILogger<SyncController> logger, ApplicationDbContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }

        /// <summary>Start the synchronization process.</summary>
        /// <param name="username">The username to access the API.</param>
        /// <param name="password">The password to access the API.</param>
        /// <returns>A boolean result to indicate the process has succesfully started and finished.</returns>
        [HttpPost]
        public IActionResult Post(GetBearerTokenRequest model)
        {
            if (ModelState.IsValid)
            {
                // Get the bearer token for the user
                var authEndpoint = AemenersolApi.GetAuthEndpoint();
                var accessToken = authEndpoint.GetAccessToken(model.Username, model.Password);

                if (string.IsNullOrEmpty(accessToken))
                    return Unauthorized();

                AemenersolApi.SetBearerToken(accessToken);

                // Get the Platform Well Endpoint API Library and get the platform wells
                var platformWellEndpoint = AemenersolApi.GetPlatformWellEndpoint();
                var platFormWells = platformWellEndpoint.GetPlatformWells(true);

                //Start the Platform Well Synchronization process
                PlatformWellSynchronizer synchronizer = new PlatformWellSynchronizer(DbContext);
                synchronizer.Sync(platFormWells);

                return Ok(true);
            }

            return BadRequest();
        }
    }
}