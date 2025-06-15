using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Repositories.EFCore.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var hasher = new PasswordHasher<User>();
            var users = new List<User>
            {
                // Mevcut 5 kullanıcı korunuyor
                new User { Id = 1, Name = "Admin User", UserName = "admin", NormalizedUserName = "ADMIN", Email = "admin@example.com", NormalizedEmail = "ADMIN@EXAMPLE.COM", PasswordHash = hasher.HashPassword(new User(), "Password123!"), SecurityStamp = Guid.NewGuid().ToString(), Role = "Admin" },
                new User { Id = 2, Name = "Dr. Ayşe Yılmaz", UserName = "ayseyilmaz", NormalizedUserName = "AYSEYILMAZ", Email = "ayse.yilmaz@example.com", NormalizedEmail = "AYSE.YILMAZ@EXAMPLE.COM", PasswordHash = hasher.HashPassword(new User(), "Password123!"), SecurityStamp = Guid.NewGuid().ToString(), Role = "Doctor", Specialization = "Kardiyoloji", ClinicId = 1, HospitalId = 1 },
                new User { Id = 3, Name = "Dr. Mehmet Kaya", UserName = "mehmetkaya", NormalizedUserName = "MEHMETKAYA", Email = "mehmet.kaya@example.com", NormalizedEmail = "MEHMET.KAYA@EXAMPLE.COM", PasswordHash = hasher.HashPassword(new User(), "Password123!"), SecurityStamp = Guid.NewGuid().ToString(), Role = "Doctor", Specialization = "Dahiliye", ClinicId = 2, HospitalId = 1 },
                new User { Id = 4, Name = "Ali Veli", UserName = "aliveli", NormalizedUserName = "ALIVELI", Email = "ali.veli@example.com", NormalizedEmail = "ALI.VELI@EXAMPLE.COM", PasswordHash = hasher.HashPassword(new User(), "Password123!"), SecurityStamp = Guid.NewGuid().ToString(), Role = "Patient" },
                new User { Id = 5, Name = "Zeynep Çelik", UserName = "zeynepcelik", NormalizedUserName = "ZEYNEPCELIK", Email = "zeynep.celik@example.com", NormalizedEmail = "ZEYNEP.CELIK@EXAMPLE.COM", PasswordHash = hasher.HashPassword(new User(), "Password123!"), SecurityStamp = Guid.NewGuid().ToString(), Role = "Patient" }
            };

            var firstNames = new[] { "Ahmet", "Mehmet", "Mustafa", "Ayşe", "Fatma", "Zeynep", "Ali", "Hasan", "Emine", "Murat" };
            var lastNames = new[] { "Yılmaz", "Kaya", "Demir", "Çelik", "Şahin", "Yıldız", "Öztürk", "Aydın", "Arslan", "Doğan" };
            var random = new Random();

            int userId = 6;
            int totalDistricts = 15;
            int hospitalsPerDistrict = 3;
            int clinicsPerDistrict = 10;
            int totalHospitals = totalDistricts * hospitalsPerDistrict;
            var specializations = new[] { "Kardiyoloji", "Dahiliye", "Ortopedi", "Pediatri", "Dermatoloji", "Göz", "KBB", "Üroloji", "Kadın Doğum", "Nöroloji" };

            for (int hospitalId = 1; hospitalId <= totalHospitals; hospitalId++)
            {
                int districtId = ((hospitalId - 1) / hospitalsPerDistrict) + 1;
                
                for (int clinicIndex = 0; clinicIndex < clinicsPerDistrict; clinicIndex++)
                {
                    int uniqueClinicId = (districtId - 1) * clinicsPerDistrict + clinicIndex + 1;
                    var specializationName = specializations[clinicIndex];

                    for (int i = 0; i < 3; i++)
                    {
                        var firstName = firstNames[random.Next(firstNames.Length)];
                        var lastName = lastNames[random.Next(lastNames.Length)];
                        var doctorName = $"Dr. {firstName} {lastName}";
                        var email = $"{firstName.ToLower()}.{lastName.ToLower()}{userId}@example.com"
                            .Replace("ş", "s").Replace("ç", "c").Replace("ğ", "g")
                            .Replace("ü", "u").Replace("ö", "o").Replace("ı", "i");

                        users.Add(new User
                        {
                            Id = userId++,
                            Name = doctorName,
                            UserName = email,
                            NormalizedUserName = email.ToUpper(),
                            Email = email,
                            NormalizedEmail = email.ToUpper(),
                            PasswordHash = hasher.HashPassword(new User(), "Password123!"),
                            SecurityStamp = Guid.NewGuid().ToString(),
                            Role = "Doctor",
                            Specialization = specializationName,
                            ClinicId = uniqueClinicId,
                            HospitalId = hospitalId
                        });
                    }
                }
            }
            
            builder.HasData(users);
        }
    }
} 