using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Repositories.EFCore.Config
{
    public class HospitalConfig : IEntityTypeConfiguration<Hospital>
    {
        public void Configure(EntityTypeBuilder<Hospital> builder)
        {
            var hospitals = new List<Hospital>();
            int hospitalId = 1;
            
            var districtNames = new Dictionary<int, string>
            {
                {1, "Kadıköy"}, {2, "Beşiktaş"}, {3, "Şişli"}, {4, "Fatih"}, {5, "Üsküdar"},
                {6, "Çankaya"}, {7, "Keçiören"}, {8, "Yenimahalle"}, {9, "Mamak"}, {10, "Etimesgut"},
                {11, "Konak"}, {12, "Bornova"}, {13, "Karşıyaka"}, {14, "Buca"}, {15, "Çiğli"}
            };

            for (int districtId = 1; districtId <= 15; districtId++)
            {
                var districtName = districtNames[districtId];
                hospitals.Add(new Hospital { Id = hospitalId++, Name = $"{districtName} Devlet Hastanesi", DistrictId = districtId });
                hospitals.Add(new Hospital { Id = hospitalId++, Name = $"{districtName} Şehir Hastanesi", DistrictId = districtId });
                hospitals.Add(new Hospital { Id = hospitalId++, Name = $"Özel {districtName} Sağlık Merkezi", DistrictId = districtId });
            }
            
            builder.HasData(hospitals);
        }
    }
}