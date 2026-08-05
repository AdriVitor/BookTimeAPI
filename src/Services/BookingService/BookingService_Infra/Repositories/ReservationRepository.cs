using BookingService_Domain.Entities;
using BookingService_Infra.Context;
using BookingService_Infra.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingService_Infra.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ContextDb _context;
        public ReservationRepository(ContextDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllByResource(int idPlace)
        {
            return await _context.Reservations
                .Where(r => r.IdResource == idPlace)
                .ToListAsync();
        }

        public async Task<Reservation> GetById(int idReservation)
        {
            return await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == idReservation);
        }

        public async Task<int> Add(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return reservation.Id;
        }

        public async Task Update(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(Reservation reservation)
        {
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsExistsReservation(int idResource, DateTime startDate, DateTime endDate, int? ignoreReservationId = null)
            =>  await _context.Reservations
                              .AnyAsync(x => x.IdResource == idResource
                                          && x.StartDate >= startDate
                                          && x.EndDate <= endDate
                                          && (ignoreReservationId == null || x.Id != ignoreReservationId));
    }
}
