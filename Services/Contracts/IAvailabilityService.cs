using Entities.DataTransferObjects;

namespace Services.Contracts;

public interface IAvailabilityService
{
    Task<List<AvailabilityDto>> GetAvailableSlots(int doctorId, DateTime startDate, DateTime endDate);
    Task<List<AvailabilityDto>> SearchAvailabilityAsync(SearchAvailabilityRequest request);

}