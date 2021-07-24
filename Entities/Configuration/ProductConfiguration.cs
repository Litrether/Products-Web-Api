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
                    Cost = 49.99,
                    CategoryId = 2,
                    ProviderId = 2,
                },
                new Product
                {
                    Id = 2,
                    Name = "Sweatpants",
                    Description = "Essential collection",
                    Cost = 25.00,
                    CategoryId = 3,
                    ProviderId = 3,
                },
                new Product
                {
                    Id = 3,
                    Name = "T-shirt",
                    Description = "Bright color",
                    Cost = 15.00,
                    CategoryId = 3,
                    ProviderId = 3,
                },
                new Product
                {
                    Id = 4,
                    Name = "Shirt",
                    Description = "A lot of choice color",
                    Cost = 9.99,
                    CategoryId = 3,
                    ProviderId = 3,
                },
                new Product
                {
                    Id = 5,
                    Name = "Hat",
                    Description = "Good hat",
                    Cost = 15.00,
                    CategoryId = 3,
                    ProviderId = 3,
                },
                new Product
                {
                    Id = 6,
                    Name = "Telegram bot",
                    Description = "You can yourself configure this bot",
                    Cost = 95.00,
                    CategoryId = 5,
                    ProviderId = 6,
                },
                new Product
                {
                    Id = 7,
                    Name = "Macbook ",
                    Description = "Good choice for programmer",
                    Cost = 1100.00,
                    CategoryId = 6,
                    ProviderId = 7,
                }
            );
        }
    }
}
