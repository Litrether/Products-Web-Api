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
                    LocationUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2245.0599144050398!2d37.633201316046446!3d55.75746139909995!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x46b54bc6463a7e1f%3A0xd68e89e31b1d749d!2sUnderdog!5e0!3m2!1sru!2sby!4v1629892468792!5m2!1sru!2sby"
                },
                new Provider
                {
                    Id = 2,
                    Name = "Atha Makina",
                    LocationUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d24955.706297501256!2d27.025704972171763!3d38.56917789967588!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x14bbd0550cc74b71%3A0x1187e14414fd380b!2sAtha%20Pack!5e0!3m2!1sru!2sby!4v1629892445596!5m2!1sru!2sby"
                },
                new Provider
                {
                    Id = 3,
                    Name = "Shirin Agro",
                    LocationUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3023.4065496478047!2d46.268293615655864!3d40.73107894438454!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x403f5703b931bbad%3A0x6961e67ff6f0fa5b!2zU0jEsFLEsE4gQUdSTw!5e0!3m2!1sru!2sby!4v1629892427348!5m2!1sru!2sby"
                },
                new Provider
                {
                    Id = 4,
                    Name = "Milk Gorki",
                    LocationUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2217.634110197547!2d43.877038416060536!3d56.23255746235498!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x414e2b0cf172c9f5%3A0x52c68ec4e6912c0c!2z0JzQvtC70L7Rh9C90LDRjyDRgNC10LrQsA!5e0!3m2!1sru!2sby!4v1629892401840!5m2!1sru!2sby"
                },
                new Provider
                {
                    Id = 5,
                    Name = "Archeda",
                    LocationUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d329288.70255430153!2d43.46470878878752!3d49.8505744089535!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x4110ae4940ccb62b%3A0xbcc910b5c1d192f1!2z0JDRgNGH0LXQtNCw!5e0!3m2!1sru!2sby!4v1629892371206!5m2!1sru!2sby"
                },
                new Provider
                {
                    Id = 6,
                    Name = "Pascual",
                    LocationUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d1514194.156913194!2d-8.354596788586328!3d42.16290088735141!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0xd2585ac9f614b59%3A0x5fa1a18a4537ab19!2sLeche%20Pascual!5e0!3m2!1sru!2sby!4v1629892347122!5m2!1sru!2sby"
                },
                new Provider
                {
                    Id = 7,
                    Name = "Javimar",
                    LocationUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2940.5620697896807!2d-8.779720084303564!3d42.5221136328442!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0xd2f6b3162dc2bb7%3A0x7393d35e4abdf3fb!2sAlimentos%20Javimar%20S.L.!5e0!3m2!1sru!2sby!4v1629892308672!5m2!1sru!2sby"
                },
                new Provider
                {
                    Id = 8,
                    Name = "MiLida",
                    LocationUrl = "https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d11191.06220539455!2d25.314657263806758!3d53.86720882934107!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x46de8adedfc9ddfd%3A0x2120bcee12937ed1!2z0JvQuNC00YHQutC40Lkg0LzQvtC70L7Rh9C90L4t0LrQvtC90YHQtdGA0LLQvdGL0Lkg0LrQvtC80LHQuNC90LDRgg!5e0!3m2!1sru!2sby!4v1629891953077!5m2!1sru!2sby"
                }
            );
        }
    }
}
