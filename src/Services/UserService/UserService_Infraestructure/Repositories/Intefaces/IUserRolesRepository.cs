using UserService_Domain.Entities;

namespace UserService_Infraestructure.Repositories.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<UserRoles?> GetAsync(int userId, int roleId);
        Task AddAsync(UserRoles entity);
        Task DeleteAsync(int userId, int roleId);
        Task DeleteByUserAsync(int userId);
        Task<IEnumerable<UserRoles>> GetAllByUserAsync(int userId);
        Task SaveChangesAsync();
    }
}


