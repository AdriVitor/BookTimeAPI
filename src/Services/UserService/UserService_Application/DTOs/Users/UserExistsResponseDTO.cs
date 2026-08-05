using System.Text.Json.Serialization;

namespace UserService_Application.DTOs.Users
{
    public class UserExistsResponseDTO
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }
    }
}
