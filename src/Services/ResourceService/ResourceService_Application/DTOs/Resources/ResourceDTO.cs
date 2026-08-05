using ResourceService_Domain.Entities;

namespace ResourceService_Application.DTOs.Resources
{
    public class ResourceDTO
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IdUf { get; set; }
        public string Address { get; set; }
    }
}
