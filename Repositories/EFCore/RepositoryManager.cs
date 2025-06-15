using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IAvailabilityRepository> _availabilityRepository;
        private readonly Lazy<IAppointmentRepository> _appointmentRepository;
        private readonly Lazy<IDropdownRepository> _dropdownRepository;
        /*private readonly Lazy<IClinicRepository> _clinicRepository;
        private readonly Lazy<ICityRepository> _cityRepository;
        private readonly Lazy<IDistrictRepository> _districtRepository;
        private readonly Lazy<IHospitalRepository> _hospitalRepository;*/
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<ILabObservationRepository> _labObservationRepository;
        private readonly Lazy<IChatSessionRepository> _chatSessionRepository;
        private readonly Lazy<IChatMessageRepository> _chatMessageRepository;
        private readonly Lazy<IUploadRepository> _uploadRepository;
        private readonly Lazy<IAnalysisResultRepository> _analysisResultRepository;
        private readonly Lazy<IAiPromptLogRepository> _aiPromptLogRepository;
        

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _availabilityRepository = new Lazy<IAvailabilityRepository>(() => new AvailabilityRepository(_context));
            _appointmentRepository = new Lazy<IAppointmentRepository>(() => new AppointmentRepository(_context));
            _dropdownRepository = new Lazy<IDropdownRepository>(() => new DropdownRepository(_context));
            /*_clinicRepository = new Lazy<IClinicRepository>(() => new ClinicRepository(_context));
            _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(_context));
            _districtRepository = new Lazy<IDistrictRepository>(() => new DistrictRepository(_context));
            _hospitalRepository = new Lazy<IHospitalRepository>(() => new HospitalRepository(_context));*/
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));
            _labObservationRepository = new Lazy<ILabObservationRepository>(() => new LabObservationRepository(_context));
            _chatSessionRepository = new Lazy<IChatSessionRepository>(() => new ChatSessionRepository(_context));
            _chatMessageRepository = new Lazy<IChatMessageRepository>(() => new ChatMessageRepository(_context));
            _uploadRepository = new Lazy<IUploadRepository>(() => new UploadRepository(_context));
            _analysisResultRepository = new Lazy<IAnalysisResultRepository>(() => new AnalysisResultRepository(_context));
            _aiPromptLogRepository = new Lazy<IAiPromptLogRepository>(() => new AiPromptLogRepository(_context));
        }

        public IAvailabilityRepository Availability => _availabilityRepository.Value;
        public IAppointmentRepository Appointment => _appointmentRepository.Value;

        public IDropdownRepository Dropdown => _dropdownRepository.Value;
        /*public IClinicRepository Clinic => _clinicRepository.Value;
        public ICityRepository City => _cityRepository.Value;
        public IDistrictRepository District => _districtRepository.Value;
        public IHospitalRepository Hospital => _hospitalRepository.Value;*/
        public IUserRepository User => _userRepository.Value;
        public ILabObservationRepository LabObservation => _labObservationRepository.Value;
        public IChatSessionRepository ChatSession => _chatSessionRepository.Value;
        public IChatMessageRepository ChatMessage => _chatMessageRepository.Value;
        public IUploadRepository Upload => _uploadRepository.Value;
        public IAnalysisResultRepository AnalysisResult => _analysisResultRepository.Value;
        public IAiPromptLogRepository AiPromptLog => _aiPromptLogRepository.Value;

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}