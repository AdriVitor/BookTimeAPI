using BookingService_Application.DTOs;
using BookingService_Application.DTOs.Reservation;
using BookingService_Domain.Entities;

namespace BookingService_Application.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllByResourceAsync(int idPlace);
        Task<Reservation> GetByIdAsync(int idReservation);
        Task AddAsync(ReservationCreateDTO dto);
        Task UpdateAsync(ReservationUpdateDTO reservationDto);
        Task<bool> DeleteAsync(int idReservation);
    }
}
