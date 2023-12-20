using KfcApi.Contexts;
using KfcApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static System.Net.Mime.MediaTypeNames;

namespace KfcApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            if (product.Image != null)
            {
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "ProductsImages");
                string uniqueFileName = $"{Guid.NewGuid().ToString()}_{product.Image.FileName}";


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    product.Image.CopyTo(fileStream);
                }
                var baseUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}";

                product.ImageUrl = $"{baseUrl}/ProductsImages/{uniqueFileName}";
            }

            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProduct(int id)
        {
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if(product == null)
            {
                return null;
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _db.Products.Include(p => p.Category).Include(p => p.HomeCategory).ToListAsync();
        }

        public async Task<Product?> GetProduct(int id)
        {
            var product = await _db.Products.Include(p => p.Category).Include(p => p.HomeCategory).FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<Product?> UpdateProduct(int id, Product product)
        {
            var existingProduct = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.HasMenu = product.HasMenu;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.HomeCategoryId = product.HomeCategoryId;
            existingProduct.Updated_at = DateTime.Now;

            await _db.SaveChangesAsync();
            return existingProduct;
        }
    }
}
