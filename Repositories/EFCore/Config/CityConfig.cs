
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
                new City { Id = 1, Name = "İstanbul" },
                new City { Id = 2, Name = "Ankara" }
            );
        }
    }
}