using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Repositories.EFCore.Config;

public class ClinicHospitalConfig : IEntityTypeConfiguration<ClinicHospital>
{
    public void Configure(EntityTypeBuilder<ClinicHospital> builder)
    {
        // Composite key'i tekrar tanımlıyoruz
        builder.HasKey(ch => new { ch.ClinicId, ch.HospitalId });

        // İlişkileri ve silme davranışını manuel olarak yapılandır
        builder.HasOne(ch => ch.Clinic)
            .WithMany(c => c.ClinicHospitals)
            .HasForeignKey(ch => ch.ClinicId)
            .OnDelete(DeleteBehavior.Cascade); // Clinic silinince ilişki kaydı silinsin

        builder.HasOne(ch => ch.Hospital)
            .WithMany(h => h.ClinicHospitals)
            .HasForeignKey(ch => ch.HospitalId)
            .OnDelete(DeleteBehavior.Restrict); // Hospital silinmeye çalışılırsa ve ilişkili Clinic varsa engelle

        var clinicHospitals = new List<ClinicHospital>();
        int totalHospitals = 45;
        int totalClinics = 10;

        // Her hastaneye 10 kliniği ata
        for (int hospitalId = 1; hospitalId <= totalHospitals; hospitalId++)
        {
            for (int clinicId = 1; clinicId <= totalClinics; clinicId++)
            {
                // Id olmadan sadece ilişkiyi kuruyoruz
                clinicHospitals.Add(new ClinicHospital { ClinicId = clinicId, HospitalId = hospitalId });
            }
        }

        builder.HasData(clinicHospitals);
    }
}