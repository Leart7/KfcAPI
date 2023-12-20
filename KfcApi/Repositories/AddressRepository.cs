using KfcApi.Contexts;
using KfcApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KfcApi.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _db;

        public AddressRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Address> CreateAddress(Address address)
        {
            await _db.Addresses.AddAsync(address);
            await _db.SaveChangesAsync();
            return address;
        }

        public async Task<Address?> DeleteAddress(int id)
        {
            var address = await _db.Addresses.FirstOrDefaultAsync(a => a.Id == id);
            if (address == null)
            {
                return null;
            }

            _db.Addresses.Remove(address);
            await _db.SaveChangesAsync();
            return address;
        }

        public async Task<Address?> GetAddress(int id)
        {
            var address = await _db.Addresses.FirstOrDefaultAsync(a => a.Id == id);
            if(address == null)
            {
                return null;
            }

            return address;
        }

        public async Task<List<Address>> GetAllAddresses(string userId)
        {
            return await _db.Addresses.Where(a => a.UserId == userId).ToListAsync();
        }

        public async Task<Address?> UpdateAddress(int id, Address address)
        {
            var existingAddress = await _db.Addresses.FirstOrDefaultAsync(a => a.Id == id);
            if (existingAddress == null)
            {
                return null;
            }

            existingAddress.AddressName = address.AddressName;
            existingAddress.AddressNotes = address.AddressNotes;
            existingAddress.Longitude = address.Longitude;
            existingAddress.Latitude = address.Latitude;
            existingAddress.Type = address.Type;
            existingAddress.HouseNumber = address.HouseNumber;
            existingAddress.Updated_at = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return existingAddress;

        }
    }
}
