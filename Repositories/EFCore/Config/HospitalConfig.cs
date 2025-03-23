using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class HospitalConfig : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            // İlçe-Hastane ilişkisi
            builder.HasOne(h => h.District)
                .WithMany(d => d.Hospitals)
                .HasForeignKey(h => h.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(h => h.ClinicHospitals)
                .WithOne(ch => ch.Hospital)
                .HasForeignKey(ch => ch.HospitalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Hastane-Doktor ilişkisi
            builder.HasMany(h => h.Doctors)
                .WithOne(d => d.Hospital)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data
            builder.HasData(
                new Hospital { Id = 1, Name = "İstanbul Şehir Hastanesi", DistrictId = 1 },
                new Hospital { Id = 2, Name = "Ankara Şehir Hastanesi", DistrictId = 2 }
            );
        }
    }
}