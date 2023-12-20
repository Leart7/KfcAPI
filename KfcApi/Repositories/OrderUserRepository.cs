using KfcApi.Contexts;
using KfcApi.Models;
using Microsoft.EntityFrameworkCore;

namespace KfcApi.Repositories
{
    public class OrderUserRepository : IOrderUserRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderUserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<OrderUser> CreateOrderUser(OrderUser orderUser)
        {
            await _db.OrderUsers.AddAsync(orderUser);
            await _db.SaveChangesAsync();
            return orderUser;
        }

        public async Task<OrderUser> GetLastOrderUser(string userId)
        {
            var ordersUser = _db.OrderUsers
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.Created_at) 
            .Take(1);

            var lastOrderUser = await ordersUser.SingleOrDefaultAsync(); 
            if(lastOrderUser == null)
            {
                return null;
            }

            return lastOrderUser;
        }
    }
}
