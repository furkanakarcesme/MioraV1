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
    
    public async Task<Appointment?> GetAppointmentByIdAsync(int appointmentId)
    {
        // Randevuyu çekip "Availability" dahil her şeyi ekleyebilirsiniz:
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Availability)
            .FirstOrDefaultAsync(a => a.Id == appointmentId);
    }
    
    // Yeni metot
    public async Task<List<Appointment>> GetPastAppointmentsByPatientIdAsync(int patientId)
    {
        var now = DateTime.Now; // veya DateTime.Today, vs.
        return await _context.Appointments
            .Where(a => a.PatientId == patientId)
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Availability) // <-- Yeni Eklendi
            .ToListAsync();
    }
}