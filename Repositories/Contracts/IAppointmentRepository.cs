using Entities.Models;

namespace Repositories.Contracts;

public interface IAppointmentRepository
{
    Task CreateAppointmentAsync(Appointment appointment);
    Task<List<Appointment>> GetPastAppointmentsByPatientIdAsync(int patientId);

    Task<Appointment?> GetAppointmentByIdAsync(int appointmentId);

}