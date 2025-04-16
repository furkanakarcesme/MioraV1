namespace Entities.DataTransferObjects;

public class DoctorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Opsiyonel: eğer “HospitalId” ve “ClinicId” 
    // eklemek isterseniz:
    public int? HospitalId { get; set; }
    public int? ClinicId { get; set; }

    // Diğer alanlar (Specialization vs.)
    // public string? Specialization { get; set; }
}