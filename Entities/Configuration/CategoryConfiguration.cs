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
                    Name = "Vegetables"
                },
                new Category
                {
                    Id = 2,
                    Name = "Fruits"
                },
                new Category
                {
                    Id = 3,
                    Name = "Grocery"
                },
                new Category
                {
                    Id = 4,
                    Name = "Meat"
                },
                new Category
                {
                    Id = 5,
                    Name = "Dairy Products"
                },
                new Category
                {
                    Id = 6,
                    Name = "Confectionery"
                }
            );
        }
    }
}
