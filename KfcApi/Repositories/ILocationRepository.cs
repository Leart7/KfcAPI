using KfcApi.Models;

namespace KfcApi.Repositories
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAllLocations();
        Task<Location> CreateLocation(Location location);
        Task<Location?> UpdateLocation(int id, Location location);
        Task<Location?> DeleteLocation(int id);
    }
}
