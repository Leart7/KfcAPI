using KfcApi.Contexts;
using KfcApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KfcApi.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAllOrders(string userId)
        {
            return await _db.Orders
                .Where(o => o.OrderUser.UserId == userId)
                .Include(o => o.Product)
                .GroupBy(o => o.OrderUserId)  
                .Select(group => group.First())  
                .ToListAsync();
        }

        public async Task<List<Order>> GetLastOrder(int orderUserId)
        {
            return await _db.Orders.Where(o => o.OrderUserId == orderUserId).Include(o => o.Product).ToListAsync();
        }
    }
}
