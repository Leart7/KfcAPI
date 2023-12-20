using KfcApi.Models;

namespace KfcApi.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProduct(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product?> UpdateProduct(int id, Product product);
        Task<Product?> DeleteProduct(int id);
    }
}
