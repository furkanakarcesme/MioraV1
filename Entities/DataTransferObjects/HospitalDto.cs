namespace Entities.DataTransferObjects;

public class HospitalDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int DistrictId { get; set; }
    // Opsiyonel: public int CityId { get; set; }
    // Yine cityId ekleyebilirsiniz (Hospital -> District -> City).
}