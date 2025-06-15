namespace Repositories.Contracts;

public interface IRepositoryManager
{
    IAvailabilityRepository Availability { get; }
    IAppointmentRepository Appointment { get; }
    IUserRepository User { get; }
    IDropdownRepository Dropdown { get; }
    ILabObservationRepository LabObservation { get; }
    IChatSessionRepository ChatSession { get; }
    IChatMessageRepository ChatMessage { get; }
    IUploadRepository Upload { get; }
    IAnalysisResultRepository AnalysisResult { get; }
    IAiPromptLogRepository AiPromptLog { get; }
    
    Task SaveAsync();

}