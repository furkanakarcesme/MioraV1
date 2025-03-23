using Entities.Models;

namespace Repositories.Contracts;

public interface IAppointmentRepository
{
    Task CreateAppointmentAsync(Appointment appointment);
}