
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class DistrictConfig : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            // İlçe-Klinik ilişkisi (Geri eklendi)
            builder.HasMany(d => d.Clinics)
                .WithOne(c => c.District)
                .HasForeignKey(c => c.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);  // İlçeyi silerken klinikler kalmalı

            // İlçe-Hastane ilişkisi
            builder.HasMany(d => d.Hospitals)
                .WithOne(h => h.District)
                .HasForeignKey(h => h.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);  // İlçeyi silerken hastaneler kalmalı

            // Seed Data
            builder.HasData(
                new District { Id = 1, Name = "Kadıköy", CityId = 1 },
                new District { Id = 2, Name = "Çankaya", CityId = 2 }
            );
        }
    }
}