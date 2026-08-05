using UserService_Domain.Entities;

namespace UserService_Application.DTOs
{
    public record RoleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
