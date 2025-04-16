using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;

namespace Repositories.EFCore;

public class DropdownRepository : IDropdownRepository
{
    private readonly RepositoryContext _context;

    public DropdownRepository(RepositoryContext context)
    {
        _context = context;
    }

    public async Task<List<City>> GetAllCitiesAsync()
    {
        return await _context.Cities
            .OrderBy(c => c.Name)
            .ToListAsync();
    }
    
    public async Task<List<District>> GetAllDistrictsAsync()
    {
        return await _context.Districts
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    public async Task<List<Clinic>> GetAllClinicsAsync()
    {
        return await _context.Clinics
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<List<District>> GetDistrictsAsync(int? cityId)
    {
        var query = _context.Districts.AsQueryable();
        if (cityId.HasValue)
            query = query.Where(d => d.CityId == cityId.Value);

        return await query
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    public async Task<List<Clinic>> GetClinicsAsync(int cityId, int? districtId)
    {
        // cityId => District.CityId
        // DistrictId opsiyonelse ekle
        // Sonra Clinic tablosunu filtrele
        var query = _context.Clinics
            .Include(c => c.District) // ThenInclude(d => d.City) if needed
            .AsQueryable();

        // cityId -> c.District.CityId == cityId
        query = query.Where(c => c.District.CityId == cityId);

        if (districtId.HasValue)
            query = query.Where(c => c.DistrictId == districtId.Value);

        return await query
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<List<Hospital>> GetHospitalsAsync(int cityId, int? districtId, int clinicId)
    {
        // city + district -> hospital.District.CityId
        // clinicId -> Many-to-many via ClinicHospital? 
        // or if hospital has a direct ref to clinic? This depends on your design.

        // Basit varsayım: hospital.District.CityId == cityId
        // DistrictId (opsiyonel)
        // "clinicId" => hospital.ClinicHospitals.Any(ch => ch.ClinicId == clinicId)
        var query = _context.Hospitals
            .Include(h => h.District)
            .Include(h => h.ClinicHospitals)
            .AsQueryable();

        query = query.Where(h => h.District.CityId == cityId);

        if (districtId.HasValue)
            query = query.Where(h => h.DistrictId == districtId.Value);

        // Many-to-Many
        query = query.Where(h => h.ClinicHospitals.Any(ch => ch.ClinicId == clinicId));

        return await query
            .OrderBy(h => h.Name)
            .ToListAsync();
    }

    public async Task<List<User>> GetDoctorsAsync(int hospitalId, int clinicId)
    {
        var query = _context.Users
            .Where(u =>
                u.Role == "Doctor" &&
                u.HospitalId == hospitalId &&
                u.ClinicId == clinicId
            );

        return await query
            .OrderBy(u => u.Name)
            .ToListAsync();
    }
}