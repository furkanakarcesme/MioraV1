using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.Contracts;
using Repositories.EFCore;


namespace Presentation.Controllers
{
    /*
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
            // 1. Slot zaten alınmış mı kontrol et
            var slot = await _manager.Availability.GetAvailabilityByDoctorAndTime(request.DoctorId, request.AppointmentDate, request.StartTime);

            if (slot == null || slot.IsBooked)
            {
                return BadRequest(new { error = "Bu slot zaten dolu veya geçerli değil." });
            }

            // 2. Randevuyu oluştur
            var appointment = new Appointment
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                AppointmentDate = request.AppointmentDate,
                IsCanceled = false
            };

            await _manager.Appointment.CreateAppointmentAsync(appointment);

            // 3. Slotu rezerve et
            slot.IsBooked = true;
            await _manager.SaveAsync();

            return Ok(new { message = "Randevu başarıyla oluşturuldu.", appointment });
        }
    }
    */
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

            // 3) Sonra normal slot kontrol ve randevu oluşturma akışı...
            var slot = await _manager.Availability.GetAvailabilityByDoctorAndTime(
                request.DoctorId, 
                request.AppointmentDate, 
                request.StartTime
            );

            if (slot == null || slot.IsBooked)
            {
                return BadRequest(new { error = "Bu slot zaten dolu veya geçerli değil." });
            }

            var appointment = new Appointment
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                AvailabilityId = slot.Id, 
                AppointmentDate = request.AppointmentDate,
                IsCanceled = false
            };

            await _manager.Appointment.CreateAppointmentAsync(appointment);
            slot.IsBooked = true;
            await _manager.SaveAsync();

            return Ok(new { message = "Randevu başarıyla oluşturuldu.", appointment });
        }
    }
        
    }