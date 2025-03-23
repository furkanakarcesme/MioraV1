namespace Entities.DataTransferObjects;

public class CancelAppointmentRequest
{
    public int AppointmentId { get; set; } // İptal edilecek randevunun ID'si
    public TimeSpan StartTime { get; set; } // Randevunun başlangıç saati (HH:mm:ss)
}