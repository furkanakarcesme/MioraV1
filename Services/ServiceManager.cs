using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAppointmentService> _appointmentService;
    private readonly Lazy<IAvailabilityService> _availabilityService;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _appointmentService = new Lazy<IAppointmentService>(() => new AppointmentManager(repositoryManager));
        _availabilityService = new Lazy<IAvailabilityService>(() => new AvailabilityManager(repositoryManager));
    }

    public IAppointmentService AppointmentService => _appointmentService.Value;
    public IAvailabilityService AvailabilityService => _availabilityService.Value;
}