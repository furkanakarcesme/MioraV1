namespace Repositories.Contracts;

public interface IRepositoryManager
{
    IAvailabilityRepository Availability { get; }
    IAppointmentRepository Appointment { get; }
    IUserRepository User { get; }
    
    Task SaveAsync();

}