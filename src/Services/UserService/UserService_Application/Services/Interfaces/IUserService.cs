using UserService_Application.DTOs.User;
using UserService_Application.DTOs.Users;
using UserService_Domain.Entities;

namespace UserService_Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<GetUserDTO?> GetByIdAsync(int id);
        Task CreateAsync(UserDTO user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<int> IsExists(string email, string password);
    }
}
