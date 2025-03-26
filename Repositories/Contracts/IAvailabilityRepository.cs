using Entities.Models;

namespace Repositories.Contracts;

public interface IAvailabilityRepository
{
    Task<List<Availability>> GetAvailabilitiesForDoctorAndDay(int doctorId, DateTime day);
    
    Task AddAvailabilitiesAsync(IEnumerable<Availability> newSlots);

    Task<Availability?> GetAvailabilityByDoctorAndTime(int doctorId, DateTime date, TimeSpan startTime);

    Task<Availability?> GetAvailabilityByIdAsync(int slotId);


}