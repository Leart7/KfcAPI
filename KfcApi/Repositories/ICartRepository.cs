using KfcApi.Models;

namespace KfcApi.Repositories
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetCartItems(string? userId);
        Task<Cart> InsertIntoCart(Cart cartItem);
        Task<Cart?> UpdateCartItem(int id, Cart newCartItem);
        Task<Cart?> DeleteCartItem(int id);
        Task<List<Cart>> DeleteCartItems(string? userId);
    }
}
