namespace AuthService_API.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }

        public LoginResponseDTO(string token)
        {
            Token = token;
        }
    }
}
