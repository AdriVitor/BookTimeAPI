using ResourceService_Domain.Entities;
using ResourceService_Infraestructure.Context;
using ResourceService_Infraestructure.Repositories.Interfaces;

namespace ResourceService_Infraestructure.Repositories
{
    public class UfRepository : IUfRepository
    {
        private ContextDb _context;
        public UfRepository(ContextDb context)
        {
            _context = context;
        }

        public Uf GetUf(int id)
        {
            return _context.Uf.FirstOrDefault(x => x.Id == id);
        }
    }
}
