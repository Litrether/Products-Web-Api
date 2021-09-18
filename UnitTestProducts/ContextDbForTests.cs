using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UnitTestProducts
{
    public class ContextDbForTests : IdentityDbContext<User>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>().HasIndex(p => new { p.Name, p.Description, p.Cost, p.CategoryId, p.ProviderId }).IsUnique();
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Description);
            modelBuilder.Entity<Product>().Property(p => p.Cost).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.CategoryId).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.ProviderId).IsRequired();

            modelBuilder.Entity<Provider>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Provider>().Property(p => p.Name).IsRequired();

            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<Provider>().Property(p => p.Name).IsRequired();

            modelBuilder.Entity<Cart>().HasIndex(c => new { c.UserId, c.ProductId }).IsUnique();
            modelBuilder.Entity<Cart>().Property(c => c.UserId).IsRequired();
            modelBuilder.Entity<Cart>().Property(c => c.ProductId).IsRequired();

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ProviderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Provider> Providers { get; set; }
    }
}
