using Aemenersol.Entity;
using System.Collections.Generic;

namespace Aemenersol.Api
{
    public interface IPlatformWellEndpoint
    {
        public List<Platform> GetPlatformWells(bool actualData = true);
    }
}