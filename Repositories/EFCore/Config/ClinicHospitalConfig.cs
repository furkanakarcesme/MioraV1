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
        int totalDistricts = 15;
        int hospitalsPerDistrict = 3;
        int clinicsPerDistrict = 10;
        int totalHospitals = totalDistricts * hospitalsPerDistrict;

        for (int hospitalId = 1; hospitalId <= totalHospitals; hospitalId++)
        {
            // Bu hastanenin hangi ilçeye ait olduğunu hesapla
            int districtId = ((hospitalId - 1) / hospitalsPerDistrict) + 1;

            // O ilçeye ait olan kliniklerin ID aralığını hesapla
            int startClinicId = (districtId - 1) * clinicsPerDistrict + 1;
            int endClinicId = districtId * clinicsPerDistrict;

            for (int clinicId = startClinicId; clinicId <= endClinicId; clinicId++)
            {
                clinicHospitals.Add(new ClinicHospital { ClinicId = clinicId, HospitalId = hospitalId });
            }
        }

        builder.HasData(clinicHospitals);
    }
}