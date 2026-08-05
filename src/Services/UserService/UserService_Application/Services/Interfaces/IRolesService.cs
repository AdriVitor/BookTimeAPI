using UserService_Application.DTOs;
using UserService_Domain.Entities;

namespace UserService_Application.Services.Interfaces
{
    public interface IRolesService
    {
        Task<Role> GetByIdAsync(int id);
        Task<IEnumerable<Role>> GetAllAsync();
        Task CreateAsync(RoleDTO roleDTO);
        Task UpdateAsync(RoleDTO roleDTO);
        Task<bool> DeleteAsync(int id);
    }

}
