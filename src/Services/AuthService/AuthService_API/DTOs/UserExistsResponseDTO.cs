using Newtonsoft.Json;

namespace AuthService_API.DTOs
{
    public class UserExistsResponseDTO
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
    }
}
