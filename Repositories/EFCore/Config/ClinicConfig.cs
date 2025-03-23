using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class ClinicConfig : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.HasOne(c => c.District)
                .WithMany(d => d.Clinics)
                .HasForeignKey(c => c.DistrictId)
                .OnDelete(DeleteBehavior.Restrict);  // İlçeyi silerken klinikler kalmalı

            builder.HasMany(c => c.ClinicHospitals)
                .WithOne(ch => ch.Clinic)
                .HasForeignKey(ch => ch.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed Data
            builder.HasData(
                new Clinic { Id = 1, Name = "Kardiyoloji", DistrictId = 1 },
                new Clinic { Id = 2, Name = "Ortopedi", DistrictId = 2 },
                new Clinic { Id = 3, Name = "Göz Hastalıkları", DistrictId = 1 },
                new Clinic { Id = 4, Name = "Çocuk Hastalıkları", DistrictId = 2 }
            );
        }
    }
}