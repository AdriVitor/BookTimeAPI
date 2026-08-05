using AuthService_API.DTOs;

namespace AuthService_API.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponseDTO> GenerateToken(LoginRequestDTO loginRequest);
    }
}
