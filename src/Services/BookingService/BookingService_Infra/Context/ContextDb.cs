using BookingService_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookingService_Infra.Context
{
    public class ContextDb : DbContext
    {
        public ContextDb() { }
        public ContextDb(DbContextOptions<ContextDb> options) : base(options) { }

        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ContextDb).Assembly);
        }
    }
}
