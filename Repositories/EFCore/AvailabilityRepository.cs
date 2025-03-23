using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.Utilities;

namespace Repositories.EFCore;

public class AvailabilityRepository : IAvailabilityRepository
{
    private readonly RepositoryContext _context;

    public AvailabilityRepository(RepositoryContext context)
    {
        _context = context;
    }

    public async Task<List<Availability>> GetAvailabilitiesWithLazyCreation(int doctorId, DateTime start, DateTime end)
    {
        var existing = await _context.Availabilities
            .Where(a => a.DoctorId == doctorId && a.AvailableDate >= start && a.AvailableDate <= end)
            .ToListAsync();

        if (existing.Any())
            return existing;

        var newSlots = SlotGenerator.GenerateSlotsForDoctor(doctorId, start, end); // static helper
        await _context.Availabilities.AddRangeAsync(newSlots);
        await _context.SaveChangesAsync();
        return newSlots;
    }
    
    /*
    public async Task<Availability?> GetAvailabilityByDoctorAndTime(int doctorId, DateTime date, TimeSpan startTime)
    {
        /*return await _context.Availabilities
            .FirstOrDefaultAsync(a => a.DoctorId == doctorId 
                                      && a.AvailableDate == date 
                                      && a.StartTime == startTime);
    
    
        return await _context.Availabilities
            .FirstOrDefaultAsync(a => 
                a.DoctorId == doctorId &&
                a.AvailableDate.Date == date.Date && // Sadece gün kısmını kontrol et
                a.StartTime == startTime && // JSON stringini TimeSpan'e çevir
                a.IsBooked == false);
    }
    */
    
    public async Task<Availability?> GetAvailabilityByDoctorAndTime(int doctorId, DateTime date, TimeSpan startTime)
    {
        // Sadece gün bazında karşılaştır: AvailableDate.Date == date.Date
        // Saat – dakika bazında karşılaştır: 
        //   a.StartTime.Hours == startTime.Hours 
        //   a.StartTime.Minutes == startTime.Minutes
        // Bu şekilde milisaniye, format vb. farkları bertaraf ediyoruz.

        return await _context.Availabilities
            .FirstOrDefaultAsync(a => 
                a.DoctorId == doctorId &&
                a.AvailableDate.Date == date.Date &&
                a.StartTime.Hours == startTime.Hours &&
                a.StartTime.Minutes == startTime.Minutes &&
                a.IsBooked == false
            );
    }

}