using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAppointmentService> _appointmentManager;
    private readonly Lazy<IAvailabilityService> _availabilityManager;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _appointmentManager = new Lazy<IAppointmentService>(() => new AppointmentManager(repositoryManager));
        _availabilityManager = new Lazy<IAvailabilityService>(() => new AvailabilityManager(repositoryManager));
    }

    public IAppointmentService AppointmentManager => _appointmentManager.Value;
    public IAvailabilityService AvailabilityManager => _availabilityManager.Value;
}