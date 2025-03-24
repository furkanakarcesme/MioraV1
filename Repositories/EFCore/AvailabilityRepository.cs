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
    

    
    // Yalnızca tek bir güne ait slotlar
    public async Task<List<Availability>> GetAvailabilitiesForDoctorAndDay(int doctorId, DateTime day)
    {
        return await _context.Availabilities
            .Where(a => a.DoctorId == doctorId && a.AvailableDate.Date == day.Date)
            .ToListAsync();
    }
    

    public async Task AddAvailabilitiesAsync(IEnumerable<Availability> newSlots)
    {
        await _context.Availabilities.AddRangeAsync(newSlots);
    }

    // Slot kontrolü: Belirli saatte boş mu?
    public async Task<Availability?> GetAvailabilityByDoctorAndTime(int doctorId, DateTime date, TimeSpan startTime)
    {
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

