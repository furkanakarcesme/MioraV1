using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config;

public class ClinicHospitalConfig : IEntityTypeConfiguration<ClinicHospital>
{
    public void Configure(EntityTypeBuilder<ClinicHospital> builder)
    {
        builder.HasKey(ch => new { ch.ClinicId, ch.HospitalId });

        builder.HasOne(ch => ch.Clinic)
            .WithMany(c => c.ClinicHospitals)
            .HasForeignKey(ch => ch.ClinicId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ch => ch.Hospital)
            .WithMany(h => h.ClinicHospitals)
            .HasForeignKey(ch => ch.HospitalId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasData(
            new ClinicHospital { ClinicId = 1, HospitalId = 1 },
            new ClinicHospital { ClinicId = 1, HospitalId = 2 },
            new ClinicHospital { ClinicId = 2, HospitalId = 1 }
        );
    }
}