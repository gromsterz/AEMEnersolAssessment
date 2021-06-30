using Aemenersol.Data.DataContext;
using Aemenersol.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Aemenersol.Data.Synchronizer
{
    public class PlatformWellSynchronizer
    {
        private readonly ApplicationDbContext DbContext;

        public PlatformWellSynchronizer(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public void Sync(List<Platform> refrenceList)
        {
            var currentPlatforms = DbContext
                .Set<Platform>()
                .Include(x => x.Wells).ToList();
            var existingPlatforms = refrenceList.Where(x => currentPlatforms.Any(y => y.PlatformId == x.PlatformId)).ToList();
            var newPlatforms = refrenceList.Where(x => !currentPlatforms.Any(y => y.PlatformId == x.PlatformId)).ToList();

            // Check and Update existing platforms
            foreach (var platform in existingPlatforms)
            {
                var existingPlatform = currentPlatforms.First(x => x.PlatformId == platform.PlatformId);

                // Update the platform
                existingPlatform.UniqueName = platform.UniqueName;
                existingPlatform.Latitude = platform.Latitude;
                existingPlatform.Longitude = platform.Longitude;
                existingPlatform.CreatedAt = platform.CreatedAt;
                existingPlatform.UpdatedAt = platform.UpdatedAt;

                var currentWells = platform.Wells.ToList();
                var existingWells = platform.Wells.Where(x => currentWells.Any(y => y.WellId == x.WellId)).ToList();
                var newWells = platform.Wells.Where(x => !currentWells.Any(y => y.WellId == x.WellId)).ToList();

                // Check and Update existing wells
                foreach (var well in existingWells)
                {
                    var existingWell = DbContext.Set<Well>().First(x => x.WellId == well.WellId);

                    // Update the well
                    existingWell.UniqueName = well.UniqueName;
                    existingWell.Latitude = well.Latitude;
                    existingWell.Longitude = well.Longitude;
                    existingWell.CreatedAt = well.CreatedAt;
                    existingWell.UpdatedAt = well.UpdatedAt;
                }

                // Add new wells
                foreach (var well in newWells)
                {
                    existingPlatform.Wells.Add(well);
                }
            }

            // Add new platforms
            foreach (var platform in newPlatforms)
            {
                DbContext.Set<Platform>().Add(platform);
            }

            DbContext.SaveChanges();
        }
    }
}