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
            // 1) Uygun slot var mı
            var slot = await _manager.Availability.GetAvailabilityByDoctorAndTime(
                request.DoctorId,
                request.AppointmentDate,
                request.StartTime
            );
            if (slot == null || slot.IsBooked)
            {
                return BadRequest(new { error = "Bu slot zaten dolu veya geçerli değil." });
            }

            // 2) Appointment oluştur
            var appointment = new Appointment
            {
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                AppointmentDate = request.AppointmentDate,
                IsCanceled = false,

                // YENİ: Foreign Key'yi slot.Id'ye eşitle
                AvailabilityId = slot.Id
            };

            await _manager.Appointment.CreateAppointmentAsync(appointment);

            // 3) Slotu rezerve et
            slot.IsBooked = true;
            await _manager.SaveAsync();

            return Ok(new { message = "Randevu başarıyla oluşturuldu.", appointment });
        }
    }
        
    }