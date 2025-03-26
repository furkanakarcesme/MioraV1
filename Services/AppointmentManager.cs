using Entities.DataTransferObjects;
using Entities.Enums;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

public class AppointmentManager : IAppointmentService
{
    private readonly IRepositoryManager _repository;

    public AppointmentManager(IRepositoryManager repository)
    {
        _repository = repository;
    }

    public async Task<AppointmentDto> BookAppointment(BookAppointmentRequest request)
    {
        // 1) Doktor / Hasta kontrolü
        var doctorUser = await _repository.User.GetUserByIdAsync(request.DoctorId);
        if (doctorUser == null || doctorUser.Role != "Doctor")
            throw new ArgumentException("Geçersiz doktor ID veya kullanıcı doktor değil.");

        var patientUser = await _repository.User.GetUserByIdAsync(request.PatientId);
        if (patientUser == null || patientUser.Role != "Patient")
            throw new ArgumentException("Geçersiz hasta ID veya kullanıcı hasta değil.");

        if (request.DoctorId == request.PatientId)
            throw new ArgumentException("Doktor ve hasta aynı olamaz.");

        // 2) Uygun slot var mı diye kontrol
        var slot = await _repository.Availability
            .GetAvailabilityByDoctorAndTime(request.DoctorId, request.AppointmentDate, request.StartTime);

        if (slot == null || slot.IsBooked)
            throw new ArgumentException("Bu slot zaten dolu veya geçerli değil.");

        // 3) Randevu oluştur
        var appointment = new Appointment
        {
            PatientId = request.PatientId,
            DoctorId = request.DoctorId,
            AvailabilityId = slot.Id,
            AppointmentDate = request.AppointmentDate,
            //IsCanceled = false
            Status = AppointmentStatus.Scheduled
        };

        // 4) DB'ye kaydet
        await _repository.Appointment.CreateAppointmentAsync(appointment);
        slot.IsBooked = true;
        await _repository.SaveAsync();

        // 5) Appointment entity → DTO
        var appointmentDto = new AppointmentDto
        {
            Id = appointment.Id,
            PatientId = appointment.PatientId,
            PatientName = patientUser.Name,
            DoctorId = appointment.DoctorId,
            DoctorName = doctorUser.Name,
            AvailabilityId = appointment.AvailabilityId,
            AppointmentDate = appointment.AppointmentDate,
            //IsCanceled = appointment.IsCanceled
            Status = appointment.Status
        };

        return appointmentDto;
    }
    
    
    
    // Yeni metot
    public async Task<List<PastAppointmentsDto>> GetPastAppointmentsByPatientId(int patientId)
    {
        // 1) Kullanıcı var mı ve gerçekten "Patient" mı?
        var user = await _repository.User.GetUserByIdAsync(patientId);
        if (user == null || user.Role != "Patient")
            throw new ArgumentException("Geçersiz hasta ID veya kullanıcı hasta değil.");

        // 2) Repo'dan geçmiş randevuları çek
        var appointments = await _repository.Appointment.GetPastAppointmentsByPatientIdAsync(patientId);

        // ==> BURADA "dinamik completed" kontrolü <==
        foreach (var app in appointments)
        {
            // Sadece Status = Scheduled ise kontrol et. 
            // (Canceled zaten iptal, Completed belki manuel set edilmiş olabilir)
            if (app.Status == AppointmentStatus.Scheduled)
            {
                // Randevu bitiş zamanı = AppointmentDate + slot endTime
                var endDateTime = app.AppointmentDate.Date 
                                  + (app.Availability?.EndTime ?? TimeSpan.Zero);

                // endDateTime şimdiki zamandan eskiyse --> Completed varsay
                if (endDateTime < DateTime.Now)
                {
                    // Bu şekilde, DB'ye kaydetmeden bellek üstünde updated
                    app.Status = AppointmentStatus.Completed;
                }
            }
        }
        
        // 3) Appointment → AppointmentDto map
        var resultDtos = appointments.Select(a => new PastAppointmentsDto
        {
            Id = a.Id,
            PatientId = a.PatientId,
            PatientName = a.Patient?.Name,
            DoctorId = a.DoctorId,
            DoctorName = a.Doctor?.Name,
            AvailabilityId = a.AvailabilityId,
            AppointmentDate = a.AppointmentDate,
            //güncelleme
            Status = a.Status,
            
            // Availability'den slot saatleri
            SlotStartTime = a.Availability?.StartTime,
            SlotEndTime = a.Availability?.EndTime
        }).ToList();

       var a =  resultDtos; 
        // 4) Sıralama: Önce AppointmentDate, sonra SlotStartTime
        resultDtos = resultDtos
            .OrderByDescending(dto => dto.AppointmentDate)        // Tarih bazında
            .ThenByDescending(dto => dto.SlotStartTime)          // Aynı günde saat bazında
            .ToList();
        
        return resultDtos;
    }
}