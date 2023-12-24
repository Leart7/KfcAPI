using KfcApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KfcApi.Contexts
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var userRoleId = "d6df3320-8552-4765-91de-800a887adbea";
            var adminRoleId = "0f79654d-c70d-436e-8fba-9ac2b6ba5413";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "User",
                    NormalizedName = "User".ToUpper(),
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }

        public DbSet<ApplicationUser> Users { get; set; }

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
