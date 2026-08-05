

using UserService_Domain.Entities;

namespace UserService_Infraestructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<int> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<int> IsExists(string email, string password);
    }
}
