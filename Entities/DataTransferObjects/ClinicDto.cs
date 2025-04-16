namespace Entities.DataTransferObjects;

public class ClinicDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int DistrictId { get; set; }
    public int CityId { get; set; }
    // eğer city id’sini de direkt tutmak isterseniz, entity’den District.CityId alarak doldurabilirsiniz.
}