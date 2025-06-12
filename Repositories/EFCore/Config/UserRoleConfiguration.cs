using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Repositories.EFCore.Config
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<int>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<int>> builder)
        {
            var userRoles = new List<IdentityUserRole<int>>
            {
                new IdentityUserRole<int> { UserId = 1, RoleId = 1 }, // Admin
                new IdentityUserRole<int> { UserId = 2, RoleId = 2 }, // Doctor
                new IdentityUserRole<int> { UserId = 3, RoleId = 2 }, // Doctor
                new IdentityUserRole<int> { UserId = 4, RoleId = 3 }, // Patient
                new IdentityUserRole<int> { UserId = 5, RoleId = 3 }  // Patient
            };

            // Yeni oluşturulan tüm doktorlara "Doctor" rolünü (RoleId = 2) ata
            int totalDoctors = 1350;
            for (int i = 0; i < totalDoctors; i++)
            {
                // UserId 6'dan başlıyor
                userRoles.Add(new IdentityUserRole<int> { UserId = 6 + i, RoleId = 2 });
            }

            builder.HasData(userRoles);
        }
    }
} 