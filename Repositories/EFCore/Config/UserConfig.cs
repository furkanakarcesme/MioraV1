using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.EFCore.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasMany(u => u.DoctorAppointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.PatientAppointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Availabilities)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);  // Cascade kaldırıldı, Restrict yapıldı.

            builder.HasOne(u => u.Clinic)
                .WithMany(c => c.Doctors) // Klinik-User ilişkisi koleksiyon eklenerek düzeltildi.
                .HasForeignKey(u => u.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Hospital)
                .WithMany(h => h.Doctors) // Hastane-User ilişkisi koleksiyon eklenerek düzeltildi.
                .HasForeignKey(u => u.HospitalId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Seed data ekleme
            builder.HasData(
                new User { Id = 1, Name = "Admin", Email = "admin@mail.com", Role = "Admin" },
                new User { Id = 2, Name = "Dr. Ahmet", Email = "ahmet@hospitalBailaname.com", Role = "Doctor", Specialization = "Cardiology", ClinicId = 1, HospitalId = 1 },
                new User { Id = 3, Name = "Mehmet", Email = "mehmet@gmail.com", Role = "Patient" }
            );
        }
    }
}


