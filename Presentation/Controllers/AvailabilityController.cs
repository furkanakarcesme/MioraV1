using Microsoft.AspNetCore.Mvc;
using Repositories.Contracts;

[ApiController]
[Route("api/availability")]
public class AvailabilityController : ControllerBase
{
    private readonly IRepositoryManager _repository;

    public AvailabilityController(IRepositoryManager repository)
    {
        _repository = repository;
    }

    [HttpGet("get-available-slots")]
    public async Task<IActionResult> GetAvailableSlots(int doctorId, DateTime startDate, DateTime endDate)
    {
        if (doctorId <= 0 || startDate > endDate)
            return BadRequest("Geçersiz parametreler.");

        // Doktor rolü kontrolü
        var doctorUser = await _repository.User.GetUserByIdAsync(doctorId);
        Console.WriteLine($"[GetAvailableSlots] doctorId={doctorId}, retrievedRole={doctorUser?.Role}");
        if (doctorUser == null)
            return BadRequest(new { error = $"Doktor bulunamadı: {doctorId}" });
        if (doctorUser.Role != "Doctor")
            return BadRequest(new { error = $"Geçersiz doktor ID veya kullanıcı doktor değil. (Rol: {doctorUser.Role})" });

        // Geçmiş tarih engeli
        if (startDate < DateTime.Today)
            startDate = DateTime.Today;

        // 10 gün sınırı
        var maxEnd = DateTime.Today.AddDays(10);
        if (endDate > maxEnd)
            endDate = maxEnd;

        var slots = await _repository.Availability.GetAvailabilitiesWithLazyCreation(doctorId, startDate, endDate);
        return Ok(slots);
    }
}