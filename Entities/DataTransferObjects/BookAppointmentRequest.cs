namespace Entities.DataTransferObjects;

public class BookAppointmentRequest
{
    public int PatientId { get; set; }  // Randevu alacak hasta ID
    public int DoctorId { get; set; }   // Randevu alınacak doktor ID
    public DateTime AppointmentDate { get; set; }  // Randevu tarihi (YYYY-MM-DD)
    public TimeSpan StartTime { get; set; } // Randevu başlangıç saati (HH:mm:ss)
}