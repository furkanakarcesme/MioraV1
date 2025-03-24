using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;
using Services.Contracts;

[ApiController]
[Route("api/availability")]
public class AvailabilityController : ControllerBase
{
    private readonly IServiceManager _service; // Değişiklik: IRepositoryManager yerine IServiceManager

    public AvailabilityController(IServiceManager service)
    {
        _service = service;
    }

    
    [HttpGet("get-available-slots")]
    public async Task<IActionResult> GetAvailableSlots(int doctorId, DateTime startDate, DateTime endDate)
    {
        // 1) En temel parametre kontrolü: (boş mu, negative mi vs.)
        if (doctorId <= 0 || startDate > endDate)
            return BadRequest("Geçersiz parametreler.");

        try
        {
            // 2) Servis çağır
            var slotsDto = await _service.AvailabilityService.GetAvailableSlots(doctorId, startDate, endDate);
            return Ok(slotsDto);
        }
        catch (ArgumentException ex)
        {
            // Service’deki hata durumlarını yakalayıp 400 döndürüyoruz.
            return BadRequest(new { error = ex.Message });
        }
    }
    
}