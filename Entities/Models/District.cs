namespace Entities.Models;

public class District
{
    public int Id { get; set; }
    public string Name { get; set; } // İlçe adı
    public int CityId { get; set; }
    public City City { get; set; } // Hangi şehre bağlı?

    public List<Clinic> Clinics { get; set; } = new List<Clinic>(); // İlçedeki klinikler
    public List<Hospital> Hospitals { get; set; } = new List<Hospital>(); // İlçedeki hastaneler
}

