namespace Entities.DataTransferObjects;

public class AppointmentDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string? PatientName { get; set; }  // Arzu ederseniz
    public int DoctorId { get; set; }
    public string? DoctorName { get; set; }
    public int AvailabilityId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public bool IsCanceled { get; set; }
}