using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.Contracts;


namespace Presentation.Controllers
{
    
    [ApiController]
    [Route("api/appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly IRepositoryManager _manager;

        public AppointmentController(IRepositoryManager manager)
        {
            _manager = manager;
        }

        
        [HttpPost("book")] 
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentRequest request)
        {
        // 1) Hasta ve Doktor rolleri kontrolü
        var doctorUser = await _manager.User.GetUserByIdAsync(request.DoctorId);
        if (doctorUser == null || doctorUser.Role != "Doctor")
        {
            return BadRequest(new { error = "Geçersiz doktor ID veya kullanıcı doktor değil." });
        }

        var patientUser = await _manager.User.GetUserByIdAsync(request.PatientId);
        if (patientUser == null || patientUser.Role != "Patient")
        {
            return BadRequest(new { error = "Geçersiz hasta ID veya kullanıcı hasta değil." });
        }

        // 2) Aynı ID ile randevu oluşturulmasın
    if (request.DoctorId == request.PatientId)
    {
        return BadRequest(new { error = "Doktor ve hasta aynı olamaz." });
    }

    // 3) Slot kontrolü: Slot var mı, başkası tarafından alınmış mı?
    var slot = await _manager.Availability.GetAvailabilityByDoctorAndTime(
        request.DoctorId,
        request.AppointmentDate,
        request.StartTime
    );

    if (slot == null || slot.IsBooked)
    {
        return BadRequest(new { error = "Bu slot zaten dolu veya geçerli değil." });
    }

    // 4) Randevu oluştur
    var appointment = new Appointment
    {
        PatientId = request.PatientId,
        DoctorId = request.DoctorId,
        AvailabilityId = slot.Id,
        AppointmentDate = request.AppointmentDate,
        IsCanceled = false
    };

    // 5) Veritabanına kaydet
    await _manager.Appointment.CreateAppointmentAsync(appointment);
    slot.IsBooked = true; // Seçilen slot artık dolu
    await _manager.SaveAsync();

    // 6) Dönüşte self-referencing loop yaşamamak için DTO kullan
    var appointmentDto = new AppointmentDto
    {
        Id = appointment.Id,
        PatientId = appointment.PatientId,
        PatientName = patientUser.Name,   // varsa ekleyebilirsiniz
        DoctorId = appointment.DoctorId,
        DoctorName = doctorUser.Name,     // varsa ekleyebilirsiniz
        AvailabilityId = appointment.AvailabilityId,
        AppointmentDate = appointment.AppointmentDate,
        IsCanceled = appointment.IsCanceled
    };

    // 7) Dönüş
    return Ok(new
    {
        message = "Randevu başarıyla oluşturuldu.",
        appointment = appointmentDto
    });
    
        }    
    }
        
}