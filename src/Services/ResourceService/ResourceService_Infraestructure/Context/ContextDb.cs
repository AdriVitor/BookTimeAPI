using Microsoft.EntityFrameworkCore;
using ResourceService_Domain.Entities;

namespace ResourceService_Infraestructure.Context
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options) { }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Uf> Uf { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ContextDb).Assembly);
        }
    }
}
