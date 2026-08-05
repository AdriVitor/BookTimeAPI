using AuthService_API.DTOs;
using AuthService_API.Services.Interfaces;
using Communication.Http.Core.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService_API.Services
{
    public class LoginService : ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientService _httpClientService;
        public LoginService(IConfiguration configuration, IHttpClientService requests)
        {
            _configuration = configuration;
            _httpClientService = requests;   
        }

        public async Task<LoginResponseDTO> GenerateToken(LoginRequestDTO loginRequest)
        {
            var response = await _httpClientService.Post<LoginRequestDTO, UserExistsResponseDTO>(loginRequest, string.Concat(_configuration["InternalServices:UrlBaseUserService"], _configuration["InternalServices:UrlUserExists"]));
            if (response is { Id: 0 })
                throw new Exception("Login ou senha incorretos");

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, response.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:ExpirationMinutes"])),
                    signingCredentials: credentials
                );

            return new LoginResponseDTO(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
