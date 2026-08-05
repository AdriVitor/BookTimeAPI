using BookingService_Domain.Entities;

namespace BookingService_Infra.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllByResource(int idPlace);
        Task<Reservation> GetById(int idReservation);
        Task<int> Add(Reservation reservation);
        Task Update(Reservation reservation);
        Task<bool> Delete(Reservation reservation);
        Task<bool> IsExistsReservation(int idResource, DateTime startDate, DateTime endDate, int? ignoreReservationId = null);
    }
}
