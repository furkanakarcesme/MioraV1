namespace Entities.DataTransferObjects;

public class PastAppointmentsDto : AppointmentDto
{
    // Ek: Availability üzerinden gelen slot saat bilgileri
    public TimeSpan? SlotStartTime { get; set; }
    public TimeSpan? SlotEndTime { get; set; }
}