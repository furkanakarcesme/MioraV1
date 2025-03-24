using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class AppointmentManager : IAppointmentService
{
    private readonly IRepositoryManager _manager;

    public AppointmentManager(IRepositoryManager manager)
    {
        _manager = manager;
    }

    
    
}