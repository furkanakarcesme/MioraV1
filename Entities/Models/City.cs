namespace Entities.Models;

public class City
{
    public int Id { get; set; }
    public string Name { get; set; } // Şehir adı

    public List<District> Districts { get; set; } = new List<District>(); // Şehirdeki ilçeler
    
}