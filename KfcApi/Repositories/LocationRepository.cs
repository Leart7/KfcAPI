using KfcApi.Contexts;
using KfcApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KfcApi.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _db;

        public LocationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Location> CreateLocation(Location location)
        {
            await _db.Locations.AddAsync(location);
            await _db.SaveChangesAsync();
            return location;
        }

        public async Task<Location?> DeleteLocation(int id)
        {
            var location = await _db.Locations.FirstOrDefaultAsync(l => l.Id == id);
            if(location == null)
            {
                return null;
            }
            _db.Remove(location);
            await _db.SaveChangesAsync();
            return location;
        }

        public async Task<List<Location>> GetAllLocations()
        {
            return await _db.Locations.ToListAsync();
        }

        public async Task<Location?> UpdateLocation(int id, Location location)
        {
            var existingLocation = await _db.Locations.FirstOrDefaultAsync(l => l.Id == id);
            if (existingLocation == null)
            {
                return null;
            }

            existingLocation.Name = location.Name;
            existingLocation.Latitude = location.Latitude;
            existingLocation.Longitude = location.Longitude;
            existingLocation.City = location.City;
            existingLocation.OpeningHour = location.OpeningHour;
            existingLocation.ClosingHour = location.ClosingHour;
            existingLocation.Updated_at = DateTime.Now;

            await _db.SaveChangesAsync();
            return existingLocation;
        }
    }
}
