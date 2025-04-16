namespace Entities.DataTransferObjects;

public class DistrictDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int CityId { get; set; }
    // Opsiyonel: public string CityName { get; set; } = string.Empty;
    // Eğer City adını da dönmek isterseniz ekleyebilirsiniz.
}