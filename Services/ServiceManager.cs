using AutoMapper;
using Entities.DataTransferObjects;
using Repositories.Contracts;
using Services.Contracts;

namespace Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IAppointmentService> _appointmentManager;
    private readonly Lazy<IAvailabilityService> _availabilityManager;
    private readonly Lazy<IDropdownService> _dropdownManager;
    
    // Yeni servisler
    private readonly Lazy<IPdfDiagnosticsService> _pdfDiagnosticsManager;
    private readonly Lazy<IChatService> _chatManager;
    private readonly Lazy<IQuickPromptService> _quickPromptManager;
    private readonly Lazy<IXRayDiagnosisService> _xrayDiagnosisManager;

    public IRepositoryManager Repositories { get; }

    public ServiceManager(IRepositoryManager repositoryManager,
                          IPythonPdfClient pythonPdfClient,
                          IPythonXrayClient pythonXrayClient,
                          IImageStorageService imageStorage,
                          IPdfStorageService pdfStorage,
                          IGptClientService gptClient,
                          IQuickPromptService quickPromptService)
    {
        Repositories = repositoryManager;

        _appointmentManager = new Lazy<IAppointmentService>(() => new AppointmentManager(repositoryManager));
        _availabilityManager = new Lazy<IAvailabilityService>(() => new AvailabilityManager(repositoryManager));
        _dropdownManager = new Lazy<IDropdownService>(() => new DropdownManager(repositoryManager));
        
        _pdfDiagnosticsManager = new Lazy<IPdfDiagnosticsService>(() => 
            new PdfDiagnosticsManager(repositoryManager, pythonPdfClient, quickPromptService, gptClient, pdfStorage));
        
        _chatManager = new Lazy<IChatService>(() => 
            new ChatManager(repositoryManager, quickPromptService, gptClient));
            
        _quickPromptManager = new Lazy<IQuickPromptService>(() => quickPromptService);

        _xrayDiagnosisManager = new Lazy<IXRayDiagnosisService>(() => 
            new XRayDiagnosisManager(repositoryManager, imageStorage, pythonXrayClient, gptClient, quickPromptService));
    }

    public IAppointmentService AppointmentManager => _appointmentManager.Value;
    public IAvailabilityService AvailabilityManager => _availabilityManager.Value;
    public IDropdownService DropdownManager => _dropdownManager.Value;
    
    public IPdfDiagnosticsService PdfDiagnostics => _pdfDiagnosticsManager.Value;
    public IChatService Chat => _chatManager.Value;
    public IQuickPromptService QuickPrompt => _quickPromptManager.Value;
    public IXRayDiagnosisService XRayDiagnosis => _xrayDiagnosisManager.Value;
}