namespace Entities.DataTransferObjects;

public class BookAppointmentRequest
{
    // public int PatientId { get; set; }  // Randevu alacak hasta ID
    // public int DoctorId { get; set; }   // Randevu alınacak doktor ID
    // public DateTime AppointmentDate { get; set; }  // Randevu tarihi (YYYY-MM-DD)
    // public TimeSpan StartTime { get; set; } // Randevu başlangıç saati (HH:mm:ss)
    
    
    public int PatientId { get; set; }   // Randevu alacak hasta
    public int SlotId { get; set; }      // Kullanıcının seçtiği Availabilty kaydı

    // Eğer isterseniz yine DoctorId tutabilirsiniz, 
    // ama genelde "SlotId" üzerinden DoctorId vs. teyit edilebilir.
    
}

