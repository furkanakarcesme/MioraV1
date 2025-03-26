namespace Entities.DataTransferObjects;

public class CancelAppointmentRequest
{
    public int AppointmentId { get; set; } // İptal edilecek randevunun ID'si
    public int AvailabilityId { get; set; }
    public DateTime AppointmentDate { get; set; }  // Randevu tarihi (YYYY-MM-DD)
    public TimeSpan StartTime { get; set; } // Randevunun başlangıç saati (HH:mm:ss)
    
    // burayı düşüneceğiz
}