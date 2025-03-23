namespace Entities.Models;

public class ClinicHospital
{
    public int ClinicId { get; set; }
    public Clinic Clinic { get; set; }

    public int HospitalId { get; set; }
    public Hospital Hospital { get; set; }
}