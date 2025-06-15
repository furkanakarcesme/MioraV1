using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore;

public class LabObservationRepository : RepositoryBase<LabObservation>, ILabObservationRepository
{
    public LabObservationRepository(RepositoryContext context) : base(context)
    {
    }
} 