using Repositories.Contracts;

namespace Repositories.EFCore
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly Lazy<IAvailabilityRepository> _availabilityRepository;
        private readonly Lazy<IAppointmentRepository> _appointmentRepository;
        private readonly Lazy<IUserRepository> _userRepository;

        /*private readonly Lazy<IClinicRepository> _clinicRepository;
        private readonly Lazy<ICityRepository> _cityRepository;
        private readonly Lazy<IDistrictRepository> _districtRepository;
        private readonly Lazy<IHospitalRepository> _hospitalRepository;
        */

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _availabilityRepository = new Lazy<IAvailabilityRepository>(() => new AvailabilityRepository(_context));
            _appointmentRepository = new Lazy<IAppointmentRepository>(() => new AppointmentRepository(_context));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(_context));

            /*_clinicRepository = new Lazy<IClinicRepository>(() => new ClinicRepository(_context));
            _cityRepository = new Lazy<ICityRepository>(() => new CityRepository(_context));
            _districtRepository = new Lazy<IDistrictRepository>(() => new DistrictRepository(_context));
            _hospitalRepository = new Lazy<IHospitalRepository>(() => new HospitalRepository(_context));*/
        }

        public IAvailabilityRepository Availability => _availabilityRepository.Value;
        public IAppointmentRepository Appointment => _appointmentRepository.Value;
        public IUserRepository User => _userRepository.Value;

        /*public IClinicRepository Clinic => _clinicRepository.Value;
        public ICityRepository City => _cityRepository.Value;
        public IDistrictRepository District => _districtRepository.Value;
        public IHospitalRepository Hospital => _hospitalRepository.Value;*/

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}