using BookingService_Application.DTOs;
using BookingService_Application.DTOs.Reservation;
using BookingService_Application.Services.Interfaces;
using BookingService_Domain.Entities;
using BookingService_Domain.Entities.Enums;
using BookingService_Infra.Repositories.Interfaces;
using Communication.MessageBus.Core.Abstractions;
using Communication.MessageBus.DTOs;

namespace BookingService_Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ISendMessageService _sendMessageService;
        public ReservationService(IReservationRepository reservationRepository, ISendMessageService sendMessageService)
        {
            _reservationRepository = reservationRepository;
            _sendMessageService = sendMessageService;
        }

        public async Task AddAsync(ReservationCreateDTO dto)
        {
            var existsReservation = await _reservationRepository.IsExistsReservation(dto.IdResource, dto.StartDate, dto.EndDate);
            if (existsReservation)
                throw new Exception("Essa data não está disponível, escolha outra por favor");

            var reservation = new Reservation(dto.IdResource, 
                                              dto.IdCustomer,
                                              dto.StartDate,
                                              dto.EndDate,
                                              dto.Observation,
                                              (int)StatusReservationEnum.Pending);


            var reservationId = await _reservationRepository.Add(reservation);

            await _sendMessageService.SendMessage(new UserValidatedRequestDTO()
            {
                UserId = dto.IdCustomer,
                ReservationId = reservationId,
            }, "user-validate-queue");

            await _sendMessageService.SendMessage(new ResourceValidatedRequestDTO()
            {
                ResourceId = dto.IdResource,
                ReservationId = reservationId
            }, "resource-validate-queue");
        }

        public async Task<bool> DeleteAsync(int idReservation)
        {
            var reservation = await GetByIdAsync(idReservation);
            
            return reservation is null ? 
                                  false :
                                  await _reservationRepository.Delete(reservation);                   
        }

        public Task<IEnumerable<Reservation>> GetAllByResourceAsync(int idPlace)
        {
            return _reservationRepository.GetAllByResource(idPlace);
        }

        public Task<Reservation> GetByIdAsync(int idReservation)
        {
            return _reservationRepository.GetById(idReservation);
        }

        public async Task UpdateAsync(ReservationUpdateDTO reservationDto)
        {
            var reservation = await _reservationRepository.GetById(reservationDto.Id);
            if (reservation is null)
                throw new Exception("Reserva não encontrada");

            if (reservationDto.StartDate != reservation.StartDate || reservationDto.EndDate != reservation.EndDate)
                await ValidateIfExistsReservation(reservation.IdResource, reservationDto.StartDate, reservationDto.EndDate, reservation.Id);

            reservation.UpdateSchedule(reservationDto.StartDate, reservationDto.EndDate, reservationDto.Observation);

            await _reservationRepository.Update(reservation);
        }

        private async Task ValidateIfExistsReservation(int idResource, DateTime startDate, DateTime endDate, int? ignoreReservationId = null)
        {
            var existsReservation = await _reservationRepository.IsExistsReservation(idResource, startDate, endDate, ignoreReservationId);
            if (existsReservation)
                throw new Exception("Essa data não está disponível, escolha outra por favor");
        }
    }
}
