namespace Entities.Models;

public class Appointment
{
    public int Id { get; set; }

    public int PatientId { get; set; }
    public User Patient { get; set; }

    public int DoctorId { get; set; }
    public User Doctor { get; set; }

    public int AvailabilityId { get; set; }  // Yeni Availability bağlantısı eklendi.
    public Availability Availability { get; set; }

    public DateTime AppointmentDate { get; set; }
    public bool IsCanceled { get; set; } = false;
}


/*
public class Appointment
{
    public int Id { get; set; }

    public int PatientId { get; set; }
    public User Patient { get; set; } // Hasta ID'si (User tablosundan)

    public int DoctorId { get; set; }
    public User Doctor { get; set; } // Doktor ID'si (User tablosundan)

    public DateTime AppointmentDate { get; set; } // Randevu tarihi
    public bool IsCanceled { get; set; } = false; // Hasta/Doktor/Admin iptal etti mi?
}
*/