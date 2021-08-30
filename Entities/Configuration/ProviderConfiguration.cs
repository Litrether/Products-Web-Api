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
                    Name = "Underdog",
                    LocationLat = 53.89019010647972m,
                    LocationLong = 27.575736202063215m,
                },
                new Provider
                {
                    Id = 2,
                    Name = "Atha Makina",
                    LocationLat = 38.54213540495325m,
                    LocationLong = 27.033468297986936m,
                },
                new Provider
                {
                    Id = 3,
                    Name = "Shirin Agro",
                    LocationLat = 40.73105861912476m,
                    LocationLong = 46.27047156919906m,
                },
                new Provider
                {
                    Id = 4,
                    Name = "Milk Gorki",
                    LocationLat = 54.26659741177842m,
                    LocationLong = 30.98771355605172m,
                },
                new Provider
                {
                    Id = 5,
                    Name = "Archeda",
                    LocationLat = 49.764727469041816m,
                    LocationLong = 43.65468679640968m,
                },
                new Provider
                {
                    Id = 6,
                    Name = "Pascual",
                    LocationLat = 47.46106041862809m,
                    LocationLong = -122.26236529486663m,
                },
                new Provider
                {
                    Id = 7,
                    Name = "Javimar",
                    LocationLat = 39.16566430628417m,
                    LocationLong = -0.2430474019997804m,
                },
                new Provider
                {
                    Id = 8,
                    Name = "MiLida",
                    LocationLat = 10.158678793639453m,
                    LocationLong = -10.753070951045318m,
                }
            );
        }
    }
}
