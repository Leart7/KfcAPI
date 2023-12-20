using KfcApi.Models;

namespace KfcApi.Repositories
{
    public interface IOrderUserRepository
    {
        Task<OrderUser> CreateOrderUser(OrderUser orderUser);
        Task<OrderUser> GetLastOrderUser(string userId);
    }
}
