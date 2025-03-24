namespace Entities.DataTransferObjects;

public class AvailabilityDto
{
    public int Id { get; set; }
    public int DoctorId { get; set; }
    public string? DoctorName { get; set; }  // İstersek doktorun adını da dönebiliriz
    public DateTime AvailableDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsBooked { get; set; }
}