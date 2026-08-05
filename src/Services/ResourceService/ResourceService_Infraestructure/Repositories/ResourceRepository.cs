using Microsoft.EntityFrameworkCore;
using ResourceService_Domain.Entities;
using ResourceService_Infraestructure.Context;
using ResourceService_Infraestructure.Repositories.Interfaces;

namespace ResourceService_Infraestructure.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ContextDb _context;

        public ResourceRepository(ContextDb context)
        {
            _context = context;
        }

        public async Task<Resource?> GetByIdAsync(int id)
        {
            return await _context.Resources
                                 .Include(r => r.Uf)
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Resource>> GetAllAsync()
        {
            var resources = await (from re in _context.Resources
                            join uf in _context.Uf on re.IdUf equals uf.Id
                            select new Resource()
                            {
                                Id = re.Id,
                                IdUser = re.IdUser,
                                Name = re.Name,
                                Description = re.Description,
                                IdUf = re.IdUf,
                                Address = re.Address,
                                CreatedAt = re.CreatedAt,
                                Uf = uf
                            }).Distinct().ToListAsync();

            return resources;
        }

        public async Task AddAsync(Resource resource)
        {
            resource.CreatedAt = DateTime.UtcNow;
            await _context.Resources.AddAsync(resource);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Resource resource)
        {
            _context.Resources.Update(resource);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var resource = await GetByIdAsync(id);
            if (resource != null)
            {
                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Resources.AnyAsync(r => r.Id == id);
        }
    }
}
