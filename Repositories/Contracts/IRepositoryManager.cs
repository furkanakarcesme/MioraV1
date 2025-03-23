namespace Repositories.Contracts;

public interface IRepositoryManager
{
    IAvailabilityRepository Availability { get; }
    IAppointmentRepository Appointment { get; }
    Task SaveAsync();

}