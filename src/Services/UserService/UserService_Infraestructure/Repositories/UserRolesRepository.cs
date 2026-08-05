using Microsoft.EntityFrameworkCore;
using UserService_Domain.Entities;
using UserService_Infraestructure.Context;
using UserService_Infraestructure.Repositories.Interfaces;

namespace UserService_Infraestructure.Repositories
{
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly ContextDb _context;

        public UserRolesRepository(ContextDb context)
        {
            _context = context;
        }

        public async Task<UserRoles?> GetAsync(int userId, int roleId)
        {
            return await _context.UserRoles
                .FirstOrDefaultAsync(ur => ur.IdUser == userId && ur.IdRole == roleId);
        }

        public async Task AddAsync(UserRoles entity)
        {
            await _context.UserRoles.AddAsync(entity);
        }

        public async Task DeleteAsync(int userId, int roleId)
        {
            var entity = await GetAsync(userId, roleId);
            if (entity != null)
                _context.UserRoles.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteByUserAsync(int userId)
        {
            var userRoles = await GetAllByUserAsync(userId);
            if(userRoles != null)
                _context.UserRoles.RemoveRange(userRoles);
        }

        public async Task<IEnumerable<UserRoles>> GetAllByUserAsync(int userId)
        {
            return _context.UserRoles.Where(ur => ur.IdUser == userId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
