using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects;

public class SearchAvailabilityRequest
{
    // Şehir veya District zorunlu olsun diyorsanız [Required] koyabilirsiniz
    [Required]
    public int CityId { get; set; }       
    public int? DistrictId { get; set; }
    
    public int? HospitalId { get; set; }
    [Required]
    public int ClinicId { get; set; }
    public int? DoctorId { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}