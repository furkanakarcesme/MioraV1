using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

namespace Presentation.Controllers;

[ApiController]
[Route("api/availability")]
public class AvailabilityController : ControllerBase
{
    private readonly IAvailabilityRepository _availabilityRepository;

    public AvailabilityController(IAvailabilityRepository availabilityRepository)
    {
        _availabilityRepository = availabilityRepository;
    }

    [HttpGet("get-available-slots")]
    public async Task<IActionResult> GetAvailableSlots(int doctorId, DateTime startDate, DateTime endDate)
    {
        if (doctorId <= 0 || startDate > endDate)
        {
            return BadRequest("Geçersiz parametreler.");
        }

        var slots = await _availabilityRepository.GetAvailabilitiesWithLazyCreation(doctorId, startDate, endDate);
        return Ok(slots);
    }
}