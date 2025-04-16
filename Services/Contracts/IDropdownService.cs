using Entities.DataTransferObjects;

namespace Services.Contracts;

public interface IDropdownService
{
    Task<List<CityDto>> GetCitiesAsync();
    Task<List<DistrictDto>> GetDistrictsAsync(int? cityId);
    Task<List<ClinicDto>> GetClinicsAsync(int cityId, int? districtId);
    Task<List<HospitalDto>> GetHospitalsAsync(int cityId, int? districtId, int clinicId);
    Task<List<DoctorDto>> GetDoctorsAsync(int hospitalId, int clinicId);
    
    Task<List<DistrictDto>> GetAllDistrictsAsync();
    Task<List<ClinicDto>> GetAllClinicsAsync();
}

