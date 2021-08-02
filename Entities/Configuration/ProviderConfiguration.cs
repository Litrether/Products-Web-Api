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
                    Name = "Underdog"
                },
                new Provider
                {
                    Id = 2,
                    Name = "Atha Makina"
                },
                new Provider
                {
                    Id = 3,
                    Name = "Shirin Agro"
                },
                new Provider
                {
                    Id = 4,
                    Name = "Milk Gorki"
                },
                new Provider
                {
                    Id = 5,
                    Name = "Archeda"
                },
                new Provider
                {
                    Id = 6,
                    Name = "Pascual"
                },
                new Provider
                {
                    Id = 7,
                    Name = "Javimar"
                },
                new Provider
                {
                    Id = 8,
                    Name = "MiLida"
                }
            );
        }
    }
}
