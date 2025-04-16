using Entities.Models;

namespace Repositories.Contracts;

public interface IDropdownRepository
{
    Task<List<City>> GetAllCitiesAsync();
    Task<List<District>> GetDistrictsAsync(int? cityId);
    Task<List<Clinic>> GetClinicsAsync(int cityId, int? districtId);
    Task<List<Hospital>> GetHospitalsAsync(int cityId, int? districtId, int clinicId);
    Task<List<User>> GetDoctorsAsync(int hospitalId, int clinicId);
    
    Task<List<District>> GetAllDistrictsAsync();
    Task<List<Clinic>> GetAllClinicsAsync();
}