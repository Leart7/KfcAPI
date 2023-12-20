using KfcApi.Models;

namespace KfcApi.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(Order order);
        Task<List<Order>> GetAllOrders(string userId);
        Task<List<Order>> GetLastOrder(int orderUserId);
    }
}
