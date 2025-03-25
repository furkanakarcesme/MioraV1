using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;


namespace Repositories.EFCore
{
    public class UserRepository : IUserRepository
    {
        private readonly RepositoryContext _context;
        
        public UserRepository(RepositoryContext context)
        {
            _context = context;
        }
        
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetDoctorsByFiltersAsync(int cityId,int clinicId, int? districtId, int? hospitalId, int? doctorId)
        {
            // Doktorları çekiyoruz, Hospital ve Clinic ile District'lerini de include ediyoruz.
            var query = _context.Users
                .Where(u => u.Role == "Doctor")
                .Include(u => u.Hospital).ThenInclude(h => h.District)
                .Include(u => u.Clinic).ThenInclude(c => c.District)
                .AsQueryable();

            // 1) cityId zorunlu → Doktorun kliniğinin ilçesindeki CityId eşleşmeli
            //    (Hospital tarafında city kontrolü yapmıyoruz, çünkü Clinic mandatory)
            query = query.Where(u =>
                u.Clinic != null &&
                u.Clinic.District != null &&
                u.Clinic.District.CityId == cityId
            );

            // 2) districtId opsiyonel
            if (districtId.HasValue)
            {
                query = query.Where(u =>
                    u.Clinic != null &&
                    u.Clinic.DistrictId == districtId.Value
                );
            }

            // 3) hospitalId opsiyonel
            //    “Doktor HospitalId’si şu ise” şeklinde
            if (hospitalId.HasValue)
            {
                query = query.Where(u => u.HospitalId == hospitalId.Value);
            }

            // 4) clinicId ZORUNLU → u.ClinicId eşleşmesi
            //    Dolayısıyla if (clinicId.HasValue) kontrol yok, direkt koşul
            query = query.Where(u => u.ClinicId == clinicId);

            // 5) doctorId opsiyonel
            if (doctorId.HasValue)
            {
                query = query.Where(u => u.Id == doctorId.Value);
            }

            // Sorguyu çalıştırıp liste döndür
            return await query.ToListAsync();
        }

        
    }
}