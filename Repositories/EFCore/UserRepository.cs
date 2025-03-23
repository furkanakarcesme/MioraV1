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
    }
}