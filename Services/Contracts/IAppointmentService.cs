using Entities.DataTransferObjects;
using Entities.Models;

namespace Services.Contracts;

public interface IAppointmentService
{
    Task<AppointmentDto> BookAppointment(BookAppointmentRequest request);

    Task<List<PastAppointmentsDto>> GetPastAppointmentsByPatientId(int patientId);
    
    Task<AppointmentDto> CancelAppointmentAsync(CancelAppointmentRequest request);
    
}