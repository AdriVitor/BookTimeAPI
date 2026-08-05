using ResourceService_Domain.Entities;

namespace ResourceService_Infraestructure.Repositories.Interfaces
{
    public interface IUfRepository
    {
        public Uf GetUf(int id);
    }
}
