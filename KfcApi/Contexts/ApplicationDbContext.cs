using KfcApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KfcApi.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<HomeCategory> HomeCategories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<AddOn> AddOns { get; set; }
        public DbSet<MenuAddOn> MenuAddOns { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<OrderUser> OrderUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
