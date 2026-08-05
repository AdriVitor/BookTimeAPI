using UserService_Application.DTOs.UserRoles;
using UserService_Application.Services.Interfaces;
using UserService_Infraestructure.Repositories.Interfaces;

namespace UserRoles.Services
{
    public class UserRolesService : IUserRolesService
    {
        private readonly IUserRolesRepository _repository;

        public UserRolesService(IUserRolesRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserService_Domain.Entities.UserRoles?> GetAsync(int userId, int roleId)
        {
            return await _repository.GetAsync(userId, roleId);
        }

        public async Task AddAsync(UserRolesDTO dto)
        {
            var userRole = await GetAsync(dto.IdUser, dto.IdRole);
            if (userRole != null)
                return;

            await _repository.AddAsync(new UserService_Domain.Entities.UserRoles(dto.IdUser, dto.IdRole));
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int userId, int roleId)
        {
            await _repository.DeleteAsync(userId, roleId);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserService_Domain.Entities.UserRoles>> GetAllByUser(int userId)
        {
            return await _repository.GetAllByUserAsync(userId);
        }
    }
}
