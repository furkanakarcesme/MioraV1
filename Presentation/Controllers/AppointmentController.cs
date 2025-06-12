using Microsoft.AspNetCore.Mvc;
using Entities.DataTransferObjects;
using Services.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    //[Authorize]
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
                // Service'de fırlatılan hata
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
        
        [HttpPost("cancel")]
        public async Task<IActionResult> CancelAppointment([FromBody] CancelAppointmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.AppointmentManager.CancelAppointmentAsync(request);
                return Ok(new 
                { 
                    message = "Randevu başarıyla iptal edildi.", 
                    appointment = result 
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        
        
    }
        //Local Commit Deneme 
}