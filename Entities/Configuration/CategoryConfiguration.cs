using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
            (
                new Category
                {
                    Id = 1,
                    Name = "Phone"
                },
                new Category
                {
                    Id = 2,
                    Name = "Keyboard"
                },
                new Category
                {
                    Id = 3,
                    Name = "Clothes"
                },
                new Category
                {
                    Id = 4,
                    Name = "Car"
                },
                new Category
                {
                    Id = 5,
                    Name = "Software"
                },
                new Category
                {
                    Id = 6,
                    Name = "Laptop"
                }
            );
        }
    }
}
