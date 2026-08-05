using UserService_Domain.Entities;

namespace UserService_Infraestructure.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        Task<Role> GetByIdAsync(int id);
        Task<IEnumerable<Role>> GetAllAsync();
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeleteAsync(int id);
    }
}
