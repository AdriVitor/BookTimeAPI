using Microsoft.EntityFrameworkCore;
using UserService_Domain.Entities;
using UserService_Infraestructure.Context;
using UserService_Infraestructure.Repositories.Interfaces;

namespace UserService_Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ContextDb _context;
        public UserRepository(ContextDb context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<int> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<int> IsExists(string email, string password)
        {
            return await (from u in _context.Users
                        where
                            u.Email.ToLower() == email.ToLower() &&
                            u.Password == password
                        select
                            u.Id).FirstOrDefaultAsync();
        }
    }
}
