using UserService_Application.DTOs;
using UserService_Application.Services.Interfaces;
using UserService_Domain.Entities;
using UserService_Infraestructure.Repositories.Interfaces;

namespace UserService_Application.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _rolesRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _rolesRepository.GetAllAsync();
        }

        public async Task CreateAsync(RoleDTO roleDTO)
        {           
            await _rolesRepository.AddAsync(new Role(roleDTO.Name));
        }

        public async Task UpdateAsync(RoleDTO roleDTO)
        {
            var existingRole = await _rolesRepository.GetByIdAsync(roleDTO.Id);
            if (existingRole == null) throw new Exception("Não foi possível realizar a atualização");

            existingRole.Name = roleDTO.Name;
            await _rolesRepository.UpdateAsync(existingRole);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _rolesRepository.GetByIdAsync(id);
            if (role == null) return false;

            await _rolesRepository.DeleteAsync(id);
            return true;
        }
    }

}
