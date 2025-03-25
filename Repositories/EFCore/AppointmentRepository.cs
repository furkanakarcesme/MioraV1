using Entities.Models;
using Microsoft.EntityFrameworkCore;
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
    
    // Yeni metot
    public async Task<List<Appointment>> GetPastAppointmentsByPatientIdAsync(int patientId)
    {
        var now = DateTime.Now; // veya DateTime.Today, vs.
        return await _context.Appointments
            // .Where(a => 
            //     a.PatientId == patientId &&
            //     a.AppointmentDate < now &&
            //     a.IsCanceled == false
            // )
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .ToListAsync();
    }
}