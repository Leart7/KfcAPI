using KfcApi.Models;

namespace KfcApi.Repositories
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAddresses(string userId);
        Task<Address?> GetAddress(int id);
        Task<Address> CreateAddress(Address address);
        Task<Address?> UpdateAddress(int id, Address address);
        Task<Address?> DeleteAddress(int id);
    }
}
