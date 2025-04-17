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
    // Kadıköy (DistrictId = 1)
    new Hospital { Id =   1, Name = "Kadıköy Şehir Hastanesi",      DistrictId = 1 },
    new Hospital { Id =   2, Name = "Kadıköy Devlet Hastanesi",     DistrictId = 1 },
    new Hospital { Id =   3, Name = "Kadıköy Özel Hastanesi",       DistrictId = 1 },

    // Beşiktaş (DistrictId = 2)
    new Hospital { Id =   4, Name = "Beşiktaş Şehir Hastanesi",     DistrictId = 2 },
    new Hospital { Id =   5, Name = "Beşiktaş Devlet Hastanesi",    DistrictId = 2 },
    new Hospital { Id =   6, Name = "Beşiktaş Özel Hastanesi",      DistrictId = 2 },

    // Üsküdar (DistrictId = 3)
    new Hospital { Id =   7, Name = "Üsküdar Şehir Hastanesi",      DistrictId = 3 },
    new Hospital { Id =   8, Name = "Üsküdar Devlet Hastanesi",     DistrictId = 3 },
    new Hospital { Id =   9, Name = "Üsküdar Özel Hastanesi",       DistrictId = 3 },

    // Bakırköy (DistrictId = 4)
    new Hospital { Id =  10, Name = "Bakırköy Şehir Hastanesi",     DistrictId = 4 },
    new Hospital { Id =  11, Name = "Bakırköy Devlet Hastanesi",    DistrictId = 4 },
    new Hospital { Id =  12, Name = "Bakırköy Özel Hastanesi",      DistrictId = 4 },

    // Şişli (DistrictId = 5)
    new Hospital { Id =  13, Name = "Şişli Şehir Hastanesi",        DistrictId = 5 },
    new Hospital { Id =  14, Name = "Şişli Devlet Hastanesi",       DistrictId = 5 },
    new Hospital { Id =  15, Name = "Şişli Özel Hastanesi",         DistrictId = 5 },

    // Çankaya (DistrictId = 6)
    new Hospital { Id =  16, Name = "Çankaya Şehir Hastanesi",      DistrictId = 6 },
    new Hospital { Id =  17, Name = "Çankaya Devlet Hastanesi",     DistrictId = 6 },
    new Hospital { Id =  18, Name = "Çankaya Özel Hastanesi",       DistrictId = 6 },

    // Keçiören (DistrictId = 7)
    new Hospital { Id =  19, Name = "Keçiören Şehir Hastanesi",     DistrictId = 7 },
    new Hospital { Id =  20, Name = "Keçiören Devlet Hastanesi",    DistrictId = 7 },
    new Hospital { Id =  21, Name = "Keçiören Özel Hastanesi",      DistrictId = 7 },

    // Mamak (DistrictId = 8)
    new Hospital { Id =  22, Name = "Mamak Şehir Hastanesi",        DistrictId = 8 },
    new Hospital { Id =  23, Name = "Mamak Devlet Hastanesi",       DistrictId = 8 },
    new Hospital { Id =  24, Name = "Mamak Özel Hastanesi",         DistrictId = 8 },

    // Yenimahalle (DistrictId = 9)
    new Hospital { Id =  25, Name = "Yenimahalle Şehir Hastanesi",  DistrictId = 9 },
    new Hospital { Id =  26, Name = "Yenimahalle Devlet Hastanesi", DistrictId = 9 },
    new Hospital { Id =  27, Name = "Yenimahalle Özel Hastanesi",   DistrictId = 9 },

    // Altındağ (DistrictId = 10)
    new Hospital { Id =  28, Name = "Altındağ Şehir Hastanesi",     DistrictId = 10 },
    new Hospital { Id =  29, Name = "Altındağ Devlet Hastanesi",    DistrictId = 10 },
    new Hospital { Id =  30, Name = "Altındağ Özel Hastanesi",      DistrictId = 10 },

    // Konak (DistrictId = 11)
    new Hospital { Id =  31, Name = "Konak Şehir Hastanesi",        DistrictId = 11 },
    new Hospital { Id =  32, Name = "Konak Devlet Hastanesi",       DistrictId = 11 },
    new Hospital { Id =  33, Name = "Konak Özel Hastanesi",         DistrictId = 11 },

    // Karşıyaka (DistrictId = 12)
    new Hospital { Id =  34, Name = "Karşıyaka Şehir Hastanesi",    DistrictId = 12 },
    new Hospital { Id =  35, Name = "Karşıyaka Devlet Hastanesi",   DistrictId = 12 },
    new Hospital { Id =  36, Name = "Karşıyaka Özel Hastanesi",     DistrictId = 12 },

    // Bornova (DistrictId = 13)
    new Hospital { Id =  37, Name = "Bornova Şehir Hastanesi",      DistrictId = 13 },
    new Hospital { Id =  38, Name = "Bornova Devlet Hastanesi",     DistrictId = 13 },
    new Hospital { Id =  39, Name = "Bornova Özel Hastanesi",       DistrictId = 13 },

    // Buca (DistrictId = 14)
    new Hospital { Id =  40, Name = "Buca Şehir Hastanesi",         DistrictId = 14 },
    new Hospital { Id =  41, Name = "Buca Devlet Hastanesi",        DistrictId = 14 },
    new Hospital { Id =  42, Name = "Buca Özel Hastanesi",          DistrictId = 14 },

    // Gaziemir (DistrictId = 15)
    new Hospital { Id =  43, Name = "Gaziemir Şehir Hastanesi",     DistrictId = 15 },
    new Hospital { Id =  44, Name = "Gaziemir Devlet Hastanesi",    DistrictId = 15 },
    new Hospital { Id =  45, Name = "Gaziemir Özel Hastanesi",      DistrictId = 15 },

    // Nilüfer (DistrictId = 16)
    new Hospital { Id =  46, Name = "Nilüfer Şehir Hastanesi",      DistrictId = 16 },
    new Hospital { Id =  47, Name = "Nilüfer Devlet Hastanesi",     DistrictId = 16 },
    new Hospital { Id =  48, Name = "Nilüfer Özel Hastanesi",       DistrictId = 16 },

    // Osmangazi (DistrictId = 17)
    new Hospital { Id =  49, Name = "Osmangazi Şehir Hastanesi",    DistrictId = 17 },
    new Hospital { Id =  50, Name = "Osmangazi Devlet Hastanesi",   DistrictId = 17 },
    new Hospital { Id =  51, Name = "Osmangazi Özel Hastanesi",     DistrictId = 17 },

    // Yıldırım (DistrictId = 18)
    new Hospital { Id =  52, Name = "Yıldırım Şehir Hastanesi",     DistrictId = 18 },
    new Hospital { Id =  53, Name = "Yıldırım Devlet Hastanesi",    DistrictId = 18 },
    new Hospital { Id =  54, Name = "Yıldırım Özel Hastanesi",      DistrictId = 18 },

    // Gürsu (DistrictId = 19)
    new Hospital { Id =  55, Name = "Gürsu Şehir Hastanesi",        DistrictId = 19 },
    new Hospital { Id =  56, Name = "Gürsu Devlet Hastanesi",       DistrictId = 19 },
    new Hospital { Id =  57, Name = "Gürsu Özel Hastanesi",         DistrictId = 19 },

    // Gemlik (DistrictId = 20)
    new Hospital { Id =  58, Name = "Gemlik Şehir Hastanesi",       DistrictId = 20 },
    new Hospital { Id =  59, Name = "Gemlik Devlet Hastanesi",      DistrictId = 20 },
    new Hospital { Id =  60, Name = "Gemlik Özel Hastanesi",        DistrictId = 20 },

    // Muratpaşa (DistrictId = 21)
    new Hospital { Id =  61, Name = "Muratpaşa Şehir Hastanesi",    DistrictId = 21 },
    new Hospital { Id =  62, Name = "Muratpaşa Devlet Hastanesi",   DistrictId = 21 },
    new Hospital { Id =  63, Name = "Muratpaşa Özel Hastanesi",     DistrictId = 21 },

    // Konyaaltı (DistrictId = 22)
    new Hospital { Id =  64, Name = "Konyaaltı Şehir Hastanesi",    DistrictId = 22 },
    new Hospital { Id =  65, Name = "Konyaaltı Devlet Hastanesi",   DistrictId = 22 },
    new Hospital { Id =  66, Name = "Konyaaltı Özel Hastanesi",     DistrictId = 22 },

    // Kepez (DistrictId = 23)
    new Hospital { Id =  67, Name = "Kepez Şehir Hastanesi",        DistrictId = 23 },
    new Hospital { Id =  68, Name = "Kepez Devlet Hastanesi",       DistrictId = 23 },
    new Hospital { Id =  69, Name = "Kepez Özel Hastanesi",         DistrictId = 23 },

    // Alanya (DistrictId = 24)
    new Hospital { Id =  70, Name = "Alanya Şehir Hastanesi",       DistrictId = 24 },
    new Hospital { Id =  71, Name = "Alanya Devlet Hastanesi",      DistrictId = 24 },
    new Hospital { Id =  72, Name = "Alanya Özel Hastanesi",        DistrictId = 24 },

    // Manavgat (DistrictId = 25)
    new Hospital { Id =  73, Name = "Manavgat Şehir Hastanesi",     DistrictId = 25 },
    new Hospital { Id =  74, Name = "Manavgat Devlet Hastanesi",    DistrictId = 25 },
    new Hospital { Id =  75, Name = "Manavgat Özel Hastanesi",      DistrictId = 25 },

    // Seyhan (DistrictId = 26)
    new Hospital { Id =  76, Name = "Seyhan Şehir Hastanesi",       DistrictId = 26 },
    new Hospital { Id =  77, Name = "Seyhan Devlet Hastanesi",      DistrictId = 26 },
    new Hospital { Id =  78, Name = "Seyhan Özel Hastanesi",        DistrictId = 26 },

    // Yüreğir (DistrictId = 27)
    new Hospital { Id =  79, Name = "Yüreğir Şehir Hastanesi",      DistrictId = 27 },
    new Hospital { Id =  80, Name = "Yüreğir Devlet Hastanesi",     DistrictId = 27 },
    new Hospital { Id =  81, Name = "Yüreğir Özel Hastanesi",       DistrictId = 27 },

    // Çukurova (DistrictId = 28)
    new Hospital { Id =  82, Name = "Çukurova Şehir Hastanesi",     DistrictId = 28 },
    new Hospital { Id =  83, Name = "Çukurova Devlet Hastanesi",    DistrictId = 28 },
    new Hospital { Id =  84, Name = "Çukurova Özel Hastanesi",      DistrictId = 28 },

    // Sarıçam (DistrictId = 29)
    new Hospital { Id =  85, Name = "Sarıçam Şehir Hastanesi",      DistrictId = 29 },
    new Hospital { Id =  86, Name = "Sarıçam Devlet Hastanesi",     DistrictId = 29 },
    new Hospital { Id =  87, Name = "Sarıçam Özel Hastanesi",       DistrictId = 29 },

    // Ceyhan (DistrictId = 30)
    new Hospital { Id =  88, Name = "Ceyhan Şehir Hastanesi",       DistrictId = 30 },
    new Hospital { Id =  89, Name = "Ceyhan Devlet Hastanesi",      DistrictId = 30 },
    new Hospital { Id =  90, Name = "Ceyhan Özel Hastanesi",        DistrictId = 30 },

    // Selçuklu (DistrictId = 31)
    new Hospital { Id =  91, Name = "Selçuklu Şehir Hastanesi",     DistrictId = 31 },
    new Hospital { Id =  92, Name = "Selçuklu Devlet Hastanesi",    DistrictId = 31 },
    new Hospital { Id =  93, Name = "Selçuklu Özel Hastanesi",      DistrictId = 31 },

    // Karatay (DistrictId = 32)
    new Hospital { Id =  94, Name = "Karatay Şehir Hastanesi",      DistrictId = 32 },
    new Hospital { Id =  95, Name = "Karatay Devlet Hastanesi",     DistrictId = 32 },
    new Hospital { Id =  96, Name = "Karatay Özel Hastanesi",       DistrictId = 32 },

    // Meram (DistrictId = 33)
    new Hospital { Id =  97, Name = "Meram Şehir Hastanesi",        DistrictId = 33 },
    new Hospital { Id =  98, Name = "Meram Devlet Hastanesi",       DistrictId = 33 },
    new Hospital { Id =  99, Name = "Meram Özel Hastanesi",         DistrictId = 33 },

    // Ereğli (DistrictId = 34)
    new Hospital { Id = 100, Name = "Ereğli Şehir Hastanesi",       DistrictId = 34 },
    new Hospital { Id = 101, Name = "Ereğli Devlet Hastanesi",      DistrictId = 34 },
    new Hospital { Id = 102, Name = "Ereğli Özel Hastanesi",        DistrictId = 34 },

    // Akşehir (DistrictId = 35)
    new Hospital { Id = 103, Name = "Akşehir Şehir Hastanesi",      DistrictId = 35 },
    new Hospital { Id = 104, Name = "Akşehir Devlet Hastanesi",     DistrictId = 35 },
    new Hospital { Id = 105, Name = "Akşehir Özel Hastanesi",       DistrictId = 35 },

    // Şahinbey (DistrictId = 36)
    new Hospital { Id = 106, Name = "Şahinbey Şehir Hastanesi",     DistrictId = 36 },
    new Hospital { Id = 107, Name = "Şahinbey Devlet Hastanesi",    DistrictId = 36 },
    new Hospital { Id = 108, Name = "Şahinbey Özel Hastanesi",      DistrictId = 36 },

    // Şehitkamil (DistrictId = 37)
    new Hospital { Id = 109, Name = "Şehitkamil Şehir Hastanesi",   DistrictId = 37 },
    new Hospital { Id = 110, Name = "Şehitkamil Devlet Hastanesi",  DistrictId = 37 },
    new Hospital { Id = 111, Name = "Şehitkamil Özel Hastanesi",    DistrictId = 37 },

    // Nizip (DistrictId = 38)
    new Hospital { Id = 112, Name = "Nizip Şehir Hastanesi",        DistrictId = 38 },
    new Hospital { Id = 113, Name = "Nizip Devlet Hastanesi",       DistrictId = 38 },
    new Hospital { Id = 114, Name = "Nizip Özel Hastanesi",         DistrictId = 38 },

    // İslahiye (DistrictId = 39)
    new Hospital { Id = 115, Name = "İslahiye Şehir Hastanesi",     DistrictId = 39 },
    new Hospital { Id = 116, Name = "İslahiye Devlet Hastanesi",    DistrictId = 39 },
    new Hospital { Id = 117, Name = "İslahiye Özel Hastanesi",      DistrictId = 39 },

    // Karkamış (DistrictId = 40)
    new Hospital { Id = 118, Name = "Karkamış Şehir Hastanesi",     DistrictId = 40 },
    new Hospital { Id = 119, Name = "Karkamış Devlet Hastanesi",    DistrictId = 40 },
    new Hospital { Id = 120, Name = "Karkamış Özel Hastanesi",      DistrictId = 40 },

    // Haliliye (DistrictId = 41)
    new Hospital { Id = 121, Name = "Haliliye Şehir Hastanesi",     DistrictId = 41 },
    new Hospital { Id = 122, Name = "Haliliye Devlet Hastanesi",    DistrictId = 41 },
    new Hospital { Id = 123, Name = "Haliliye Özel Hastanesi",      DistrictId = 41 },

    // Eyyübiye (DistrictId = 42)
    new Hospital { Id = 124, Name = "Eyyübiye Şehir Hastanesi",     DistrictId = 42 },
    new Hospital { Id = 125, Name = "Eyyübiye Devlet Hastanesi",    DistrictId = 42 },
    new Hospital { Id = 126, Name = "Eyyübiye Özel Hastanesi",      DistrictId = 42 },

    // Karaköprü (DistrictId = 43)
    new Hospital { Id = 127, Name = "Karaköprü Şehir Hastanesi",    DistrictId = 43 },
    new Hospital { Id = 128, Name = "Karaköprü Devlet Hastanesi",   DistrictId = 43 },
    new Hospital { Id = 129, Name = "Karaköprü Özel Hastanesi",     DistrictId = 43 },

    // Siverek (DistrictId = 44)
    new Hospital { Id = 130, Name = "Siverek Şehir Hastanesi",      DistrictId = 44 },
    new Hospital { Id = 131, Name = "Siverek Devlet Hastanesi",     DistrictId = 44 },
    new Hospital { Id = 132, Name = "Siverek Özel Hastanesi",       DistrictId = 44 },

    // Viranşehir (DistrictId = 45)
    new Hospital { Id = 133, Name = "Viranşehir Şehir Hastanesi",  DistrictId = 45 },
    new Hospital { Id = 134, Name = "Viranşehir Devlet Hastanesi", DistrictId = 45 },
    new Hospital { Id = 135, Name = "Viranşehir Özel Hastanesi",   DistrictId = 45 },

    // Akdeniz (DistrictId = 46)
    new Hospital { Id = 136, Name = "Akdeniz Şehir Hastanesi",     DistrictId = 46 },
    new Hospital { Id = 137, Name = "Akdeniz Devlet Hastanesi",    DistrictId = 46 },
    new Hospital { Id = 138, Name = "Akdeniz Özel Hastanesi",      DistrictId = 46 },

    // Mezitli (DistrictId = 47)
    new Hospital { Id = 139, Name = "Mezitli Şehir Hastanesi",     DistrictId = 47 },
    new Hospital { Id = 140, Name = "Mezitli Devlet Hastanesi",    DistrictId = 47 },
    new Hospital { Id = 141, Name = "Mezitli Özel Hastanesi",      DistrictId = 47 },

    // Toroslar (DistrictId = 48)
    new Hospital { Id = 142, Name = "Toroslar Şehir Hastanesi",    DistrictId = 48 },
    new Hospital { Id = 143, Name = "Toroslar Devlet Hastanesi",   DistrictId = 48 },
    new Hospital { Id = 144, Name = "Toroslar Özel Hastanesi",     DistrictId = 48 },

    // Yenişehir (DistrictId = 49)
    new Hospital { Id = 145, Name = "Yenişehir Şehir Hastanesi",   DistrictId = 49 },
    new Hospital { Id = 146, Name = "Yenişehir Devlet Hastanesi",  DistrictId = 49 },
    new Hospital { Id = 147, Name = "Yenişehir Özel Hastanesi",    DistrictId = 49 },

    // Tarsus (DistrictId = 50)
    new Hospital { Id = 148, Name = "Tarsus Şehir Hastanesi",      DistrictId = 50 },
    new Hospital { Id = 149, Name = "Tarsus Devlet Hastanesi",     DistrictId = 50 },
    new Hospital { Id = 150, Name = "Tarsus Özel Hastanesi",       DistrictId = 50 }
);
        }
    }
}