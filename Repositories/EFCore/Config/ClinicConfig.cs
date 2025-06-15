using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Repositories.EFCore.Config
{
    public class ClinicConfig : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            var clinicNames = new[]
            {
                "Kardiyoloji", "Dahiliye", "Ortopedi", "Pediatri", "Dermatoloji",
                "Göz", "KBB", "Üroloji", "Kadın Doğum", "Nöroloji"
            };

            var clinics = new List<Clinic>();
            int clinicId = 1;
            int districtId = 1;
            int totalCities = 3;
            int districtsPerCity = 5;
            int clinicsPerDistrict = 10;

            for (int city = 1; city <= totalCities; city++)
            {
                for (int district = 1; district <= districtsPerCity; district++)
                {
                    for (int i = 0; i < clinicsPerDistrict; i++)
                    {
                        clinics.Add(new Clinic
                        {
                            Id = clinicId++,
                            Name = clinicNames[i % clinicNames.Length],
                            DistrictId = districtId,
                            IsDeleted = false
                        });
                    }
                    districtId++;
                }
            }

            builder.HasData(clinics);
        }
    }
}