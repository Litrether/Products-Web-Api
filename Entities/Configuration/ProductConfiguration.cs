using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
            (     
                new Product
                {
                    Id = 1,
                    Name = "Mi Keyboard",
                    Description = "Best of the best keyboard in the world!",
                    Cost = 49.99m,
                    CategoryId = 2,
                    ProviderId = 2,
                },
                new Product
                {
                    Id = 2,
                    Name = "Sweatpants",
                    Description = "Essential collection",
                    Cost = 25.00m,
                    CategoryId = 3,
                    ProviderId = 3,
                },
                new Product
                {
                    Id = 3,
                    Name = "Hat",
                    Description = "Good hat",
                    Cost = 15.00m,
                    CategoryId = 3,
                    ProviderId = 3,
                }
            );
        }
    }
}
