namespace Entities.Models;

public class Hospital
{
    public int Id { get; set; }
    public string Name { get; set; } // Hastane adı
    public int DistrictId { get; set; } // Hangi ilçeye bağlı?
    public District District { get; set; }

    public List<ClinicHospital> ClinicHospitals { get; set; } = new();
    public List<User> Doctors { get; set; } = new List<User>(); // Hastanedeki doktorlar
}


/*
public class Hospital
{
    public int Id { get; set; }
    public string Name { get; set; } // Hastane adı
    public int DistrictId { get; set; } // Hangi ilçeye bağlı?
    public District District { get; set; }

    public List<Clinic> Clinics { get; set; } = new List<Clinic>(); // Hastanedeki klinikler
    public List<User> Doctors { get; set; } = new List<User>(); // Hastanedeki doktorlar
}
*/