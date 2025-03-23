namespace Entities.Models;


public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

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
}

/*
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } = "Patient"; // "Patient", "Doctor", "Admin"

    // Doktorlara özel alanlar
    public string? Specialization { get; set; } // Doktorun uzmanlık alanı
    public int? ClinicId { get; set; }
    public Clinic Clinic { get; set; }

    public int? HospitalId { get; set; } // Doktor hangi hastanede çalışıyor?
    public Hospital Hospital { get; set; }
    public List<Availability>? Availabilities { get; set; } // Doktorun müsaitlik saatleri
    
    //Hastaya özel alanlar
    public List<Appointment>? Appointments { get; set; } // Hastanın randevuları
    
    public bool IsDeleted { get; set; } = false; // Kullanıcı silindiyse
}
*/