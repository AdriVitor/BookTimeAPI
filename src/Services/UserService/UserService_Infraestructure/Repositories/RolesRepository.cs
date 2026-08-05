using Microsoft.EntityFrameworkCore;
using UserService_Domain.Entities;
using UserService_Infraestructure.Context;
using UserService_Infraestructure.Repositories.Interfaces;

namespace UserService_Infraestructure.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly ContextDb _context;

        public RolesRepository(ContextDb context)
        {
            _context = context;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _context.Roles
                //.Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles
                //.Include(r => r.Users)
                .ToListAsync();
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await GetByIdAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }

}
