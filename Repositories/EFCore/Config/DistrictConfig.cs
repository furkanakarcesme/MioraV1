using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class DistrictConfig : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasData(
                // İstanbul (CityId = 1)
                new District { Id = 1, Name = "Kadıköy", CityId = 1 },
                new District { Id = 2, Name = "Beşiktaş", CityId = 1 },
                new District { Id = 3, Name = "Şişli", CityId = 1 },
                new District { Id = 4, Name = "Fatih", CityId = 1 },
                new District { Id = 5, Name = "Üsküdar", CityId = 1 },
                // Ankara (CityId = 2)
                new District { Id = 6, Name = "Çankaya", CityId = 2 },
                new District { Id = 7, Name = "Keçiören", CityId = 2 },
                new District { Id = 8, Name = "Yenimahalle", CityId = 2 },
                new District { Id = 9, Name = "Mamak", CityId = 2 },
                new District { Id = 10, Name = "Etimesgut", CityId = 2 },
                // İzmir (CityId = 3)
                new District { Id = 11, Name = "Konak", CityId = 3 },
                new District { Id = 12, Name = "Bornova", CityId = 3 },
                new District { Id = 13, Name = "Karşıyaka", CityId = 3 },
                new District { Id = 14, Name = "Buca", CityId = 3 },
                new District { Id = 15, Name = "Çiğli", CityId = 3 }
            );
        }
    }
}