using Entities.Models;

namespace Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int id);
    }
}