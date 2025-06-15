using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class AppointmentConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            // Randevu-Hasta ilişkisi
            builder.HasOne(a => a.Patient)
                .WithMany(u => u.PatientAppointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Randevu-Doktor ilişkisi
            builder.HasOne(a => a.Doctor)
                .WithMany(u => u.DoctorAppointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(a => a.Availability)
                .WithMany()
                .HasForeignKey(a => a.AvailabilityId)
                .OnDelete(DeleteBehavior.Restrict);

            // Örnek veri
            builder.HasData(
                new Appointment
                {
                    Id = 1,
                    PatientId = 4, // Ali Veli
                    DoctorId = 2,  // Dr. Ayşe Yılmaz
                    AvailabilityId = 1,
                    AppointmentDate = DateTime.Today.AddDays(2),
                    Status = AppointmentStatus.Scheduled
                }
            );
        }
    }
}