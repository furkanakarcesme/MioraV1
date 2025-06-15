using Repositories.Contracts;

namespace Services.Contracts;

public interface IServiceManager
{
    IRepositoryManager Repositories { get; }
    IAppointmentService AppointmentManager { get; }
    IAvailabilityService AvailabilityManager { get; }
    
    IDropdownService DropdownManager { get; }

    // Yeni servisler
    IXRayDiagnosisService XRayDiagnosis { get; }
    IPdfDiagnosticsService PdfDiagnostics { get; }
    IChatService Chat { get; }
}