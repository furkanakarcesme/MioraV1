using Entities.DataTransferObjects;
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
        // 1. Parametre validasyonları (doctorId, tarih aralıkları vs.)
        if (doctorId <= 0 || startDate > endDate)
            return BadRequest("Geçersiz parametreler.");

        // 2. Doktor kontrolü
        var doctorUser = await _repository.User.GetUserByIdAsync(doctorId);
        if (doctorUser == null || doctorUser.Role != "Doctor")
            return BadRequest(new { error = "Geçersiz doktor ID veya kullanıcı doktor değil." });

        // 3. Tarih validasyonu
        if (startDate < DateTime.Today)
            startDate = DateTime.Today;
        var maxEnd = DateTime.Today.AddDays(10);
        if (endDate > maxEnd)
            endDate = maxEnd;

        // 4. Availabilities çek
        var slots = await _repository.Availability.GetAvailabilitiesWithLazyCreation(doctorId, startDate, endDate);

        // 5. Entity → DTO map işlemi
        var slotsDto = slots.Select(a => new AvailabilityDto
        {
            Id = a.Id,
            DoctorId = a.DoctorId,
            DoctorName = a.Doctor?.Name,   // Lazy/Eager Loading durumuna göre null olabilir
            AvailableDate = a.AvailableDate,
            StartTime = a.StartTime,
            EndTime = a.EndTime,
            IsDeleted = a.IsDeleted,
            IsBooked = a.IsBooked
        }).ToList();

        // 6. DTO return
        return Ok(slotsDto);
    }
    
}