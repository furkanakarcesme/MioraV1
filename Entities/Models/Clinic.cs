namespace Entities.Models;

public class Clinic
{
    public int Id { get; set; }
    public string Name { get; set; } // Klinik adı
    
    public int DistrictId { get; set; } // Hangi ilçeye bağlı?
    public District District { get; set; } 

    public List<ClinicHospital> ClinicHospitals { get; set; } = new();
    public List<User> Doctors { get; set; } = new List<User>(); // Kliniğe bağlı doktorlar

    public bool IsDeleted { get; set; } = false;
}




/*
public class Clinic
{
    public int Id { get; set; }
    public string Name { get; set; } // Klinik adı
    public int DistrictId { get; set; } // Hangi ilçeye bağlı?
    public District District { get; set; } 

    public List<Hospital> Hospitals { get; set; } = new List<Hospital>(); // Bu klinik hangi hastanelerde var?
    public List<User> Doctors { get; set; } = new List<User>(); // Kliniğe bağlı doktorlar

    public bool IsDeleted { get; set; } = false;
}
*/