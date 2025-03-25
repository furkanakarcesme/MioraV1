using Entities.DataTransferObjects;

namespace Services.Contracts;

public interface IAppointmentService
{
    Task<AppointmentDto> BookAppointment(BookAppointmentRequest request);

    Task<List<PastAppointmentsDto>> GetPastAppointmentsByPatientId(int patientId);

}