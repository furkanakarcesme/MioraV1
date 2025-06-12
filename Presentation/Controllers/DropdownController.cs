using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/dropdown")]
//[Authorize]
public class DropdownController : ControllerBase
{
    private readonly IServiceManager _service; // Değişiklik: IRepositoryManager yerine IServiceManager

    public DropdownController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("initial")]
    public async Task<IActionResult> GetInitialData()
    {
        // Tek seferde city, district, clinic al
        var cities = await _service.DropdownManager.GetCitiesAsync();      // List<CityDto>
        var districts = await _service.DropdownManager.GetAllDistrictsAsync(); // List<DistrictDto>
        var clinics = await _service.DropdownManager.GetAllClinicsAsync();     // List<ClinicDto>

        // Bir objenin içinde dönersiniz
        return Ok(new { cities, districts, clinics });
    }
    
    [HttpGet("cities")]
    public async Task<IActionResult> GetCities()
    {
        var cities = await _service.DropdownManager.GetCitiesAsync();
        return Ok(cities); // -> List<CityDto>
    }

    [HttpGet("districts")]
    public async Task<IActionResult> GetDistricts([FromQuery] int? cityId)
    {
        // cityId opsiyonelse, cityId=null => Tüm district'ler
        var districts = await _service.DropdownManager.GetDistrictsAsync(cityId);
        return Ok(districts); // -> List<DistrictDto>
    }

    [HttpGet("clinics")]
    public async Task<IActionResult> GetClinics([FromQuery] int cityId, [FromQuery] int? districtId)
    {
        // cityId zorunlu, districtId opsiyonel
        var clinics = await _service.DropdownManager.GetClinicsAsync(cityId, districtId);
        return Ok(clinics); // -> List<ClinicDto>
    }

    [HttpGet("hospitals")]
    public async Task<IActionResult> GetHospitals([FromQuery] int cityId, [FromQuery] int? districtId, [FromQuery] int clinicId)
    {
        // cityId, clinicId zorunlu; districtId opsiyonel
        var hospitals = await _service.DropdownManager.GetHospitalsAsync(cityId, districtId, clinicId);
        return Ok(hospitals); // -> List<HospitalDto>
    }

    [HttpGet("doctors")]
    public async Task<IActionResult> GetDoctors([FromQuery] int hospitalId, [FromQuery] int clinicId)
    {
        // Bu endpoint, hastane veya klinik parametresine göre doktorları listeleyecek
        var doctors = await _service.DropdownManager.GetDoctorsAsync(hospitalId, clinicId);
        return Ok(doctors); // -> List<DoctorDto>
    }
}