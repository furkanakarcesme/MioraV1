using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAppointmentService> _appointmentManager;
    private readonly Lazy<IAvailabilityService> _availabilityManager;
    private readonly Lazy<IDropdownService> _dropdownManager;

    public ServiceManager(IRepositoryManager repositoryManager)
    {
        _appointmentManager = new Lazy<IAppointmentService>(() => new AppointmentManager(repositoryManager));
        _availabilityManager = new Lazy<IAvailabilityService>(() => new AvailabilityManager(repositoryManager));
        _dropdownManager = new Lazy<IDropdownService>(() => new DropdownManager(repositoryManager));
    }

    public IAppointmentService AppointmentManager => _appointmentManager.Value;
    public IAvailabilityService AvailabilityManager => _availabilityManager.Value;
    public IDropdownService DropdownManager => _dropdownManager.Value;
}