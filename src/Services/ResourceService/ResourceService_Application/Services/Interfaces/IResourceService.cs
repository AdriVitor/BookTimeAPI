using ResourceService_Application.DTOs.Resources;
using ResourceService_Domain.Entities;

namespace ResourceService_Application.Services.Interfaces
{
    public interface IResourceService
    {
        Task<GetResourceDTO?> GetByIdAsync(int id);
        Task<List<GetResourceDTO>> GetAllAsync();
        Task CreateAsync(ResourceDTO resource);
        Task UpdateAsync(ResourceDTO resource);
        Task DeleteAsync(int id);
    }
}
