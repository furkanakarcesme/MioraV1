using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;


namespace Presentation.Controllers
{
        
    [ApiController]
    [Route("api/appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly IServiceManager _service; 

        public AppointmentController(IServiceManager service)
        {
            _service = service;
        }

        
        [HttpPost("book")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentRequest request)
        {
            // En temel parametre kontrolleri (örneğin request null mı, property'ler null mı vs.)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // 1) Servis katmanına gönder
                AppointmentDto result = await _service.AppointmentManager.BookAppointment(request);

                // 2) Başarılıysa 200 OK ve dto döndür
                return Ok(new
                {
                    message = "Randevu başarıyla oluşturuldu.",
                    appointment = result
                });
            }
            catch (ArgumentException ex)
            {
                // Service’de fırlatılan hata
                return BadRequest(new { error = ex.Message });
            }
        }
        
        
        [HttpGet("patient/{patientId}/past")]
        public async Task<IActionResult> GetPastAppointmentsByPatient(int patientId)
        {
            // 1) En temel parametre kontrolü
            if (patientId <= 0)
                return BadRequest("Geçersiz hasta ID.");

            try
            {
                // 2) Service'i çağır
                var result = await _service.AppointmentManager.GetPastAppointmentsByPatientId(patientId);

                // 3) Liste dön
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                // Role validasyonu veya user yoksa
                return BadRequest(new { error = ex.Message });
            }
        }
        
        
    }
        //Local Commit Deneme 
}