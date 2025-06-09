using Microsoft.AspNetCore.Identity;
namespace Entities.Models;


public class User : IdentityUser<int>
{
    public string Name { get; set; }

    public string Role { get; set; } = "Patient"; // "Patient", "Doctor", "Admin"

    // Doktorlara özel alanlar
    public string? Specialization { get; set; }
    public int? ClinicId { get; set; }
    public Clinic? Clinic { get; set; }
    public int? HospitalId { get; set; }
    public Hospital? Hospital { get; set; }

    // Kullanıcı-Randevu ilişkisi (Hasta olarak)
    public List<Appointment> PatientAppointments { get; set; } = new();

    // Kullanıcı-Randevu ilişkisi (Doktor olarak)
    public List<Appointment> DoctorAppointments { get; set; } = new();

    // Doktorun müsaitlikleri
    public List<Availability> Availabilities { get; set; } = new();

    public bool IsDeleted { get; set; } = false;

    // Identity için ek alanlar
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}