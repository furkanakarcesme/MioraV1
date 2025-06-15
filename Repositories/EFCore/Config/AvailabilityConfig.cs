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
                new Availability { Id = 1, DoctorId = 2, AvailableDate = DateTime.Today.AddDays(2), StartTime = new TimeSpan(9, 0, 0), EndTime = new TimeSpan(17, 0, 0), IsBooked = true },
                new Availability { Id = 2, DoctorId = 3, AvailableDate = DateTime.Today.AddDays(3), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(12, 0, 0), IsBooked = false }
            );
        }
    }
}