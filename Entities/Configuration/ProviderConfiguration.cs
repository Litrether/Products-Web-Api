using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Configuration
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasData
            (
                new Provider
                {
                    Id = 1,
                    Name = "Samsung"
                },
                new Provider
                {
                    Id = 2,
                    Name = "Xiaomi"
                },
                new Provider
                {
                    Id = 3,
                    Name = "Nike"
                },
                new Provider
                {
                    Id = 4,
                    Name = "Volvo"
                },
                new Provider
                {
                    Id = 5,
                    Name = "Audi"
                }
            );
        }
    }
}
