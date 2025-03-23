using Entities.Models;
using Repositories.Contracts;

namespace Repositories.EFCore;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly RepositoryContext _context;

    public AppointmentRepository(RepositoryContext context)
    {
        _context = context;
    }
    
    public async Task CreateAppointmentAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
    }
}