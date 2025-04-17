
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
    // İstanbul (CityId = 1)
    new District { Id =  1, Name = "Kadıköy",     CityId = 1 },
    new District { Id =  2, Name = "Beşiktaş",    CityId = 1 },
    new District { Id =  3, Name = "Üsküdar",      CityId = 1 },
    new District { Id =  4, Name = "Bakırköy",     CityId = 1 },
    new District { Id =  5, Name = "Şişli",        CityId = 1 },

    // Ankara (CityId = 2)
    new District { Id =  6, Name = "Çankaya",      CityId = 2 },
    new District { Id =  7, Name = "Keçiören",     CityId = 2 },
    new District { Id =  8, Name = "Mamak",        CityId = 2 },
    new District { Id =  9, Name = "Yenimahalle",  CityId = 2 },
    new District { Id = 10, Name = "Altındağ",     CityId = 2 },

    // İzmir (CityId = 3)
    new District { Id = 11, Name = "Konak",        CityId = 3 },
    new District { Id = 12, Name = "Karşıyaka",    CityId = 3 },
    new District { Id = 13, Name = "Bornova",      CityId = 3 },
    new District { Id = 14, Name = "Buca",         CityId = 3 },
    new District { Id = 15, Name = "Gaziemir",     CityId = 3 },

    // Bursa (CityId = 4)
    new District { Id = 16, Name = "Nilüfer",      CityId = 4 },
    new District { Id = 17, Name = "Osmangazi",    CityId = 4 },
    new District { Id = 18, Name = "Yıldırım",     CityId = 4 },
    new District { Id = 19, Name = "Gürsu",        CityId = 4 },
    new District { Id = 20, Name = "Gemlik",       CityId = 4 },

    // Antalya (CityId = 5)
    new District { Id = 21, Name = "Muratpaşa",    CityId = 5 },
    new District { Id = 22, Name = "Konyaaltı",    CityId = 5 },
    new District { Id = 23, Name = "Kepez",        CityId = 5 },
    new District { Id = 24, Name = "Alanya",       CityId = 5 },
    new District { Id = 25, Name = "Manavgat",     CityId = 5 },

    // Adana (CityId = 6)
    new District { Id = 26, Name = "Seyhan",       CityId = 6 },
    new District { Id = 27, Name = "Yüreğir",      CityId = 6 },
    new District { Id = 28, Name = "Çukurova",     CityId = 6 },
    new District { Id = 29, Name = "Sarıçam",      CityId = 6 },
    new District { Id = 30, Name = "Ceyhan",       CityId = 6 },

    // Konya (CityId = 7)
    new District { Id = 31, Name = "Selçuklu",     CityId = 7 },
    new District { Id = 32, Name = "Karatay",      CityId = 7 },
    new District { Id = 33, Name = "Meram",        CityId = 7 },
    new District { Id = 34, Name = "Ereğli",       CityId = 7 },
    new District { Id = 35, Name = "Akşehir",      CityId = 7 },

    // Gaziantep (CityId = 8)
    new District { Id = 36, Name = "Şahinbey",     CityId = 8 },
    new District { Id = 37, Name = "Şehitkamil",   CityId = 8 },
    new District { Id = 38, Name = "Nizip",        CityId = 8 },
    new District { Id = 39, Name = "İslahiye",     CityId = 8 },
    new District { Id = 40, Name = "Karkamış",     CityId = 8 },

    // Şanlıurfa (CityId = 9)
    new District { Id = 41, Name = "Haliliye",     CityId = 9 },
    new District { Id = 42, Name = "Eyyübiye",     CityId = 9 },
    new District { Id = 43, Name = "Karaköprü",    CityId = 9 },
    new District { Id = 44, Name = "Siverek",      CityId = 9 },
    new District { Id = 45, Name = "Viranşehir",   CityId = 9 },

    // Mersin (CityId = 10)
    new District { Id = 46, Name = "Akdeniz",      CityId = 10 },
    new District { Id = 47, Name = "Mezitli",      CityId = 10 },
    new District { Id = 48, Name = "Toroslar",     CityId = 10 },
    new District { Id = 49, Name = "Yenişehir",    CityId = 10 },
    new District { Id = 50, Name = "Tarsus",       CityId = 10 }
);
        }
    }
}