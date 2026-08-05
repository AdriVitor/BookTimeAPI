using ResourceService_Domain.Entities;

namespace ResourceService_Infraestructure.Repositories.Interfaces
{
    public interface IResourceRepository
    {
        Task<Resource?> GetByIdAsync(int id);
        Task<List<Resource>> GetAllAsync();
        Task AddAsync(Resource resource);
        Task UpdateAsync(Resource resource);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
