using Entities.DataTransferObjects;
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
            IsCanceled = false
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
            IsCanceled = appointment.IsCanceled
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
            IsCanceled = a.IsCanceled,
            
            // Availability'den slot saatleri
            SlotStartTime = a.Availability?.StartTime,
            SlotEndTime = a.Availability?.EndTime
        }).ToList();

        return resultDtos;
    }
}