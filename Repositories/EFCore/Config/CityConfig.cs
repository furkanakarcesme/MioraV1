
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            // Sehir-Ilce iliskisi
            builder.HasMany(c => c.Districts)
                .WithOne(d => d.City)
                .HasForeignKey(d => d.CityId);

            // Seed data ekleme
            builder.HasData(
                new City { Id = 1,  Name = "İstanbul"},
                new City { Id = 2,  Name = "Ankara"},
                new City { Id = 3,  Name = "İzmir"},
                new City { Id = 4,  Name = "Bursa"},
                new City { Id = 5,  Name = "Antalya"},
                new City { Id = 6,  Name = "Adana"},
                new City { Id = 7,  Name = "Konya"},
                new City { Id = 8,  Name = "Gaziantep"},
                new City { Id = 9,  Name = "Şanlıurfa"},
                new City { Id = 10, Name = "Mersin"}
            );
        }
    }
}