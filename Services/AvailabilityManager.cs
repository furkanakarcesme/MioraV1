using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.Contracts;
using Repositories.Utilities;
using Services.Contracts;

public class AvailabilityManager : IAvailabilityService
{
    private readonly IRepositoryManager _repository;  // veya tek tek IAvailabilityRepository, IUserRepository vb. de inject edebilirdiniz

    public AvailabilityManager(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<List<AvailabilityDto>> GetAvailableSlots(int doctorId, DateTime startDate, DateTime endDate)
    {
        // 1) Doktorun gerçekten doktor olup olmadığını kontrol et.
        var doctorUser = await _repository.User.GetUserByIdAsync(doctorId);
        if (doctorUser == null || doctorUser.Role != "Doctor")
        {
            // Tercihen custom exception da fırlatabilirsiniz.
            // Controller bu exception'ı yakalayıp uygun BadRequest döndürebilir 
            // (Global exception handling).
            throw new ArgumentException("Geçersiz doktor veya kullanıcı doktor değil.");
        }

        // 2) Gerekli tarih validasyonları
        if (startDate < DateTime.Today)
            startDate = DateTime.Today;

        var maxEnd = DateTime.Today.AddDays(10);
        if (endDate > maxEnd)
            endDate = maxEnd;

        // 3) Repo üzerinden slotları getir veya oluştur
        var slots = await GetAvailabilitiesWithLazyCreation(doctorId, startDate, endDate);

        // 4) Entity → DTO map
        var slotsDto = slots.Select(a => new AvailabilityDto
        {
            Id = a.Id,
            DoctorId = a.DoctorId,
            DoctorName = a.Doctor?.Name, // Lazy/Eager load'a göre gelebilir
            AvailableDate = a.AvailableDate,
            StartTime = a.StartTime,
            EndTime = a.EndTime,
            IsDeleted = a.IsDeleted,
            IsBooked = a.IsBooked
        }).ToList();

        return slotsDto;
    }
    
    
    public async Task<List<Availability>> GetAvailabilitiesWithLazyCreation(int doctorId, DateTime start, DateTime end)
    {
        var totalSlots = new List<Availability>();

        for (DateTime day = start.Date; day <= end.Date; day = day.AddDays(1))
        {
            // 1) O güne ait slotlar var mı?
            var daySlots = await _repository.Availability.GetAvailabilitiesForDoctorAndDay(doctorId, day);

            // 2) Yoksa yeni oluştur
            if (!daySlots.Any())
            {
                var newDaySlots = SlotGenerator.GenerateSlotsForSingleDay(doctorId, day);
                await _repository.Availability.AddAvailabilitiesAsync(newDaySlots);
                await _repository.SaveAsync(); // Veya _context.SaveChangesAsync() 
                totalSlots.AddRange(newDaySlots);
            }
            else
            {
                totalSlots.AddRange(daySlots);
            }
        }

        return totalSlots;
    }
}