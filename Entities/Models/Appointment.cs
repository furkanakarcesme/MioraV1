using Entities.Enums;

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
    
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;

    //public bool IsCanceled { get; set; } = false;
}


