namespace Entities.DataTransferObjects;

public record DoctorEarliestSlotDto
{
    public int DoctorId          { get; init; }
    public string DoctorName     { get; init; } = default!;
    public int HospitalId        { get; init; }
    public string HospitalName   { get; init; } = default!;
    public int ClinicId          { get; init; }
    public string ClinicName     { get; init; } = default!;
    public DateTime EarliestDate { get; init; }      // 17.04.2025
    public bool IsToday          { get; init; }      // “Bugün” etiketi
}