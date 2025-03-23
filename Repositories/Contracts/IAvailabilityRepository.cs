using Entities.Models;

namespace Repositories.Contracts;

public interface IAvailabilityRepository
{
    Task<List<Availability>> GetAvailabilitiesWithLazyCreation(int doctorId, DateTime start, DateTime end);
    
    Task<Availability?> GetAvailabilityByDoctorAndTime(int doctorId, DateTime date, TimeSpan startTime);

}