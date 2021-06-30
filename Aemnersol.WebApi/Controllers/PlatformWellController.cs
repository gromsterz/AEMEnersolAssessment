using Aemenersol.Data.DataContext;
using Aemenersol.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Aemnersol.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlatformWellController : ControllerBase
    {
        private readonly ILogger<SyncController> Logger;
        private readonly ApplicationDbContext DbContext;

        public PlatformWellController(ILogger<SyncController> logger, ApplicationDbContext dbContext)
        {
            Logger = logger;
            DbContext = dbContext;
        }

        /// <summary>Get a list of Platform Wells</summary>
        /// <returns>A list of Platform Wells</returns>
        [HttpGet]
        public List<Platform> Get()
        {
            return DbContext.Set<Platform>().Include(x => x.Wells).ToList();
        }
    }
}