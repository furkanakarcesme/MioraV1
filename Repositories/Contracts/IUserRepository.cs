using Entities.Models;

namespace Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<List<User>> GetDoctorsByFiltersAsync(int cityId, int clinicId, int? districtId, int? hospitalId, int? doctorId);
    }
}