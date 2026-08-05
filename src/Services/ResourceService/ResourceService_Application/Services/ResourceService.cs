using ResourceService_Application.DTOs.Resources;
using ResourceService_Application.Services.Interfaces;
using ResourceService_Domain.Entities;
using ResourceService_Infraestructure.Repositories.Interfaces;

namespace ResourceService_Application.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _repository;

        public ResourceService(IResourceRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetResourceDTO?> GetByIdAsync(int id)
        {
            var resource = await _repository.GetByIdAsync(id);
            return resource != null ? new GetResourceDTO(resource) : null;
        }

        public async Task<List<GetResourceDTO>> GetAllAsync()
        {
            var resources = await _repository.GetAllAsync();
            return resources.ConvertAll(x => new GetResourceDTO(x));
        }

        public async Task CreateAsync(ResourceDTO resourceDTO)
        {
            await _repository.AddAsync(new Resource(resourceDTO.IdUser, resourceDTO.Name, resourceDTO.Description, resourceDTO.IdUf, resourceDTO.Address));
        }

        public async Task UpdateAsync(ResourceDTO resourceDTO)
        {
            if (!await _repository.ExistsAsync(resourceDTO.Id))
                throw new KeyNotFoundException("Resource not found.");

            //ADICIONAR VALIDAÇÃO DE USUÁRIO AQUI QUANDO POSSÍVEL
            await _repository.UpdateAsync(new Resource(resourceDTO.IdUser, resourceDTO.Name, resourceDTO.Description, resourceDTO.IdUf, resourceDTO.Address));
        }

        public async Task DeleteAsync(int id)
        {
            if (!await _repository.ExistsAsync(id))
                throw new KeyNotFoundException("Resource not found.");

            await _repository.DeleteAsync(id);
        }
    }
}
