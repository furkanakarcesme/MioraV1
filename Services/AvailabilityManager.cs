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
    
    
    
    /*
    public async Task<List<AvailabilityDto>> SearchAvailabilityAsync(SearchAvailabilityRequest request)
    {
        if (request.DoctorId.HasValue && !request.HospitalId.HasValue)
            throw new ArgumentException("Hospital seçilmeden doktor seçilemez.");
    
        // 1) Tarih Aralığını Hazırla
        var start = request.StartDate ?? DateTime.Today;
        var end = request.EndDate ?? DateTime.Today.AddDays(10);
    
        // 3) 10 gün sınırı
        var maxAllowed = start.AddDays(10);
        if (end > maxAllowed)
        {
            // Seçenek A: Fazlasını kes
            end = maxAllowed;

            // Seçenek B: Hata fırlatmak isterseniz:
            //throw new ArgumentException("En fazla 10 günlük aralık seçilebilir.");
        }

        // 2) Filtreye Uygun Doktorları Bul
        //    (UserRepository içinde "GetDoctorsByFiltersAsync" gibi bir metod yazacağız.
        //     Orada city/district/hospital/clinic/doctlorId sorgusu yapabiliriz.)
        var doctors = await _repository.User.GetDoctorsByFiltersAsync(
            cityId: request.CityId,
            districtId: request.DistrictId,
            hospitalId: request.HospitalId,
            clinicId: request.ClinicId,
            doctorId: request.DoctorId
        );

        if (doctors.Count == 0)
            return new List<AvailabilityDto>(); // Hiç doktor yoksa boş döneriz

        // 3) Her Doktor İçin "Lazy Creation" Slotları Al
        //    (Böylece slot yoksa oluşturulacak.)
        var allSlots = new List<Availability>();
        foreach (var doc in doctors)
        {
            // doc.Id => DoctorId
            var slotsForDoc = await GetAvailabilitiesWithLazyCreation(doc.Id, start, end);
            allSlots.AddRange(slotsForDoc);
        }

        // 4) allSlots listesini, istenirse tek sefer daha filtreleyebiliriz:
        //    (Örneğin "sadece isDeleted=false" vs.)
        //    "start/end" zaten "lazy creation" sırasında kullanıldı, 
        //    ama tekrar "AvailableDate" >= start & <= end diyerek
        //    tam bir kesişim elde edebilirsiniz.
        var finalSlots = allSlots
            .Where(a => a.AvailableDate >= start && a.AvailableDate <= end)
            .ToList();

        // 5) Entity -> DTO
        //    Burada "a.Doctor.Hospital, a.Doctor.Clinic, a.Doctor.Name" gibi bilgilere 
        //    ihtiyaç varsa INCLUDE etmeniz gerekli:
        //    Örneğin: ".Include(a => a.Doctor).ThenInclude(d => d.Hospital)" vb. 
        //    Ama "GetAvailabilitiesWithLazyCreation" şu anda .Include yapmıyor. 
        //    Yani "a.Doctor" null gelebilir. Eager load veya lazy load ayarınıza bağlı.

        var result = finalSlots.Select(a => new AvailabilityDto
        {
            Id = a.Id,
            DoctorId = a.DoctorId,
            DoctorName = a.Doctor?.Name,
            AvailableDate = a.AvailableDate,
            StartTime = a.StartTime,
            EndTime = a.EndTime,
            IsDeleted = a.IsDeleted,
            IsBooked = a.IsBooked
            // "HospitalName = a.Doctor.Hospital.Name" veya 
            // "ClinicName = a.Doctor.Clinic.Name" diyebilirsiniz.
        }).ToList();

        // 6) Tarih + Saat sırasına göre sıralamak isterseniz
        result = result
            .OrderBy(r => r.AvailableDate)
            .ThenBy(r => r.StartTime)
            .ToList();

        return result;
        }
        
        */

    public async Task<List<AvailabilityDto>> SearchAvailabilityAsync(SearchAvailabilityRequest request)
    {
        if (request.DoctorId.HasValue && !request.HospitalId.HasValue)
            throw new ArgumentException("Hospital seçilmeden doktor seçilemez.");

        // 1) Tarih Aralığını Hazırla
        var start = request.StartDate ?? DateTime.Today;
        var end = request.EndDate ?? DateTime.Today.AddDays(10);

        // 3) 10 gün sınırı
        var maxAllowed = start.AddDays(10);
        if (end > maxAllowed)
        {
            // Seçenek A: Fazlasını kes
            end = maxAllowed;

            // Seçenek B: Hata fırlatmak isterseniz:
            //throw new ArgumentException("En fazla 10 günlük aralık seçilebilir.");
        }

        // 2) Filtreye Uygun Doktorları Bul
        //    (UserRepository içinde "GetDoctorsByFiltersAsync" gibi bir metod yazacağız.
        //     Orada city/district/hospital/clinic/doctlorId sorgusu yapabiliriz.)
        // … tarih hazırlığı aynı

        var doctors = await _repository.User.GetDoctorsByFiltersAsync(
            request.CityId, request.ClinicId,
            request.DistrictId, request.HospitalId,
            request.DoctorId);

        if (doctors.Count == 0) return new();   // hiç doktor yok

        var allSlots = new List<Availability>();

        foreach (var doc in doctors)
        {
            var s = await GetAvailabilitiesWithLazyCreation(doc.Id, start, end);
            allSlots.AddRange(s);
        }

// sadece boş slotlar kaldı ⇒ IsBooked / IsDeleted yok
        var finalSlots = allSlots
            .Where(a => a.AvailableDate >= start && a.AvailableDate <= end)
            .ToList();

//  ◂◂  “En erken slotu” listelemek istiyorsanız:
        var earliestPerDoctor = finalSlots
            .GroupBy(a => a.DoctorId)
            .Select(g => g.OrderBy(a => a.AvailableDate)
                .ThenBy(a => a.StartTime)
                .First())
            .ToList();

        var result = earliestPerDoctor.Select(a => new AvailabilityDto
            {
                Id           = a.Id,
                DoctorId     = a.DoctorId,
                DoctorName   = a.Doctor?.Name,
                AvailableDate= a.AvailableDate,
                StartTime    = a.StartTime,
                EndTime      = a.EndTime
            }).OrderBy(r => r.AvailableDate)
            .ThenBy(r => r.StartTime)
            .ToList();

        return result;
    }
}