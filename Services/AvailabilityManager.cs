using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class AvailabilityManager : IAvailabilityService
{
    private readonly IRepositoryManager _manager;

    public AvailabilityManager(IRepositoryManager manager)
    {
        _manager = manager;
    }
}