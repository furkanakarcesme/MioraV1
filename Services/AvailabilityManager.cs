using Entities.DataTransferObjects;
using Repositories.Contracts;
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
        var slots = await _repository.Availability.GetAvailabilitiesWithLazyCreation(doctorId, startDate, endDate);

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
}