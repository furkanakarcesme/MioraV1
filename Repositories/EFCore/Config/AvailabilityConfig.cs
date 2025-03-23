using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class AvailabilityConfig : IEntityTypeConfiguration<Availability>
    {
        public void Configure(EntityTypeBuilder<Availability> builder)
        {
            // Doktor-Müsaitlik ilişkisi
            builder.HasOne(a => a.Doctor)
                .WithMany(d => d.Availabilities)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data ekleme
            builder.HasData(
                new Availability { Id = 1, DoctorId = 2, AvailableDate = DateTime.Now.AddDays(2), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsDeleted = false, IsBooked = false }
            );
        }
    }
}