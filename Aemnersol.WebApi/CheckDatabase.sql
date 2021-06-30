Select b.UniqueName PlatformName, c.WellId, b.PlatformId, c.Uniquename, c.Latitude, c.Longitude, c.CreatedAt, c.UpdatedAt
from(Select PlatformId PlatformId, MAX(UpdatedAt) LastUpdatedAt
from Wells
Group by PlatformId)  a
inner join Platforms b on b.PlatformId = a.PlatformId
inner join Wells c on c.PlatformId = a.PlatformId and a.LastUpdatedAt = c.UpdatedAt
order by PlatformId