using Entities.DataTransferObjects;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class DropdownManager : IDropdownService
{
    private readonly IRepositoryManager _repository; 
    // Alternatif: Ayrı CityRepo, DistrictRepo, ClinicRepo vb. de kullanabilirsiniz,
    // ama burada tek bir "DropdownRepository" varsayıyoruz.

    public DropdownManager(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<List<CityDto>> GetCitiesAsync()
    {
        var cities = await _repository.Dropdown.GetAllCitiesAsync();
        // Map to DTO
        return cities.Select(c => new CityDto
        {
            Id = c.Id,
            Name = c.Name
        }).ToList();
    }
    
    public async Task<List<DistrictDto>> GetAllDistrictsAsync()
    {
        // Repo'dan District entity listesi çek
        var districtEntities = await _repository.Dropdown.GetAllDistrictsAsync();

        // Entity -> DTO map
        var districtDtos = districtEntities.Select(d => new DistrictDto
        {
            Id = d.Id,
            Name = d.Name,
            CityId = d.CityId
            // Opsiyonel: city name de ekleyebilirsin
        }).ToList();

        return districtDtos;
    }

    public async Task<List<ClinicDto>> GetAllClinicsAsync()
    {
        // Repo'dan Clinic entity listesi çek
        var clinicEntities = await _repository.Dropdown.GetAllClinicsAsync();

        // Entity -> DTO map
        var clinicDtos = clinicEntities.Select(c => new ClinicDto
        {
            Id = c.Id,
            Name = c.Name,
            DistrictId = c.DistrictId,
            CityId = c.District.CityId
        }).ToList();

        return clinicDtos;
    }

    public async Task<List<DistrictDto>> GetDistrictsAsync(int? cityId)
    {
        var districts = await _repository.Dropdown.GetDistrictsAsync(cityId);
        return districts.Select(d => new DistrictDto
        {
            Id = d.Id,
            Name = d.Name,
            CityId = d.CityId
        }).ToList();
    }

    public async Task<List<ClinicDto>> GetClinicsAsync(int cityId, int? districtId)
    {
        var clinics = await _repository.Dropdown.GetClinicsAsync(cityId, districtId);
        return clinics.Select(c => new ClinicDto
        {
            Id = c.Id,
            Name = c.Name,
            DistrictId = c.DistrictId,
            CityId = c.District.CityId
        }).ToList();
    }

    public async Task<List<HospitalDto>> GetHospitalsAsync(int cityId, int? districtId, int clinicId)
    {
        var hospitals = await _repository.Dropdown.GetHospitalsAsync(cityId, districtId, clinicId);
        return hospitals.Select(h => new HospitalDto
        {
            Id = h.Id,
            Name = h.Name,
            DistrictId = h.DistrictId
        }).ToList();
    }

    public async Task<List<DoctorDto>> GetDoctorsAsync(int hospitalId, int clinicId)
    {
        var doctors = await _repository.Dropdown.GetDoctorsAsync(hospitalId, clinicId);
        return doctors.Select(d => new DoctorDto
        {
            Id = d.Id,
            Name = d.Name,
            HospitalId = d.HospitalId,
            ClinicId = d.ClinicId
        }).ToList();
    }
}