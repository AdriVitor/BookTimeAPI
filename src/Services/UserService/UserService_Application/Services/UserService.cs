using UserService_Application.DTOs.User;
using UserService_Application.DTOs.Users;
using UserService_Application.Services.Interfaces;
using UserService_Domain.Entities;
using UserService_Infraestructure.Repositories.Interfaces;

namespace UserService_Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRolesRepository _roleRepository;
        private readonly IUserRolesRepository _userRoleRepository;

        public UserService(IUserRepository userRepository, 
                           IRolesRepository roleRepository,
                           IUserRolesRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<GetUserDTO?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            return user != null ? 
                    new GetUserDTO().ConvertToDTO(user) : 
                    null;
        }

        public async Task CreateAsync(UserDTO userDTO)
        {
            userDTO.Created = DateTime.UtcNow;

            var role = await _roleRepository.GetByIdAsync(userDTO.IdRole);
            if(role is null)
                throw new ArgumentException("O papel informado não foi encontrado.");           

            var idUser = await _userRepository.AddAsync(new User(userDTO.Name, userDTO.Email, userDTO.Password, userDTO.CPF, userDTO.DateOfBirth));

            await _userRoleRepository.AddAsync(new UserService_Domain.Entities.UserRoles(idUser, userDTO.IdRole));
            await _userRoleRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            user.ValidateData(user.Name, user.Email, user.Password, user.CPF, user.DateOfBirth);
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user is null)
                throw new Exception("Usuário não encontrado");

            await _userRepository.DeleteAsync(user);
            await _userRoleRepository.DeleteByUserAsync(user.Id);
        }

        public async Task<int> IsExists(string email, string password)
        {
            var id = await _userRepository.IsExists(email, password);

            return id;
        }
    }
}
