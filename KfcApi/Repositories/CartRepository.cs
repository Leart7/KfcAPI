using KfcApi.Contexts;
using KfcApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KfcApi.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;

        public CartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Cart?> DeleteCartItem(int id)
        {
            var cartItem = await _db.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (cartItem == null)
            {
                return null;
            }

            _db.Carts.Remove(cartItem);
            await _db.SaveChangesAsync();
            return cartItem;
        }

        public async Task<List<Cart>> DeleteCartItems(string? userId)
        {
            var userCartItems = await _db.Carts.Where(c => c.UserId == userId).ToListAsync();
            foreach (var cartItem in userCartItems)
            {
                _db.Carts.Remove(cartItem);
            }
            await _db.SaveChangesAsync();
            return userCartItems;
        }

        public async Task<List<Cart>> GetCartItems(string? userId)
        {
            return await _db.Carts.Where(c => c.UserId == userId).Include(c => c.Product).ToListAsync();
        }

        public async Task<Cart> InsertIntoCart(Cart cartItem)
        {
            await _db.Carts.AddAsync(cartItem); 
            await _db.SaveChangesAsync();
            return cartItem;
        }

        public async Task<Cart?> UpdateCartItem(int id, Cart newCartItem)
        {
            var existingCartItem = await _db.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if(existingCartItem == null)
            {
                return null;
            }

            existingCartItem.Quantity = newCartItem.Quantity;
            existingCartItem.Comments = newCartItem.Comments ?? "";
            existingCartItem.MenuAddOns = newCartItem.MenuAddOns ?? "";
            existingCartItem.AddOns = newCartItem.AddOns ?? "";
            existingCartItem.Updated_at = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return newCartItem;

        }
    }
}
