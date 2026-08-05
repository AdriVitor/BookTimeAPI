using UserService_Application.DTOs.UserRoles;
using UserService_Domain.Entities;

namespace UserService_Application.Services.Interfaces
{
    public interface IUserRolesService
    {
        Task<UserService_Domain.Entities.UserRoles?> GetAsync(int userId, int roleId);
        Task AddAsync(UserRolesDTO entity);
        Task DeleteAsync(int userId, int roleId);
        Task<IEnumerable<UserService_Domain.Entities.UserRoles>> GetAllByUser(int userId);
    }
}
