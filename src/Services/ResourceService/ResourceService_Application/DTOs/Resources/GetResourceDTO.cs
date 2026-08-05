using ResourceService_Application.DTOs.Uf;
using ResourceService_Domain.Entities;

namespace ResourceService_Application.DTOs.Resources
{
    public class GetResourceDTO
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int IdUf { get; set; }
        public string Address { get; set; }
        public UfDTO Uf { get; set; }

        public GetResourceDTO(Resource resource)
        {
            Id = resource.Id;
            IdUser = resource.IdUser;
            IdUf = resource.IdUf;
            Name = resource.Name;
            Description = resource.Description;
            Address = resource.Address;
            if (resource?.Uf != null)
            {
                Uf = new UfDTO()
                {
                    Id = resource.Uf.Id,
                    Name = resource.Uf.Name
                };
            }
        }
    }
}
