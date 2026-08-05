using BookingService_Application.DTOs.Reservation;
using BookingService_Domain.Entities;
using BookingService_Infra.Repositories.Interfaces;
using Communication.MessageBus.Abstractions;
using Communication.MessageBus.DTOs;
using MassTransit;
using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace BookingService_Application.Consumers
{
    public class FinishReservationConsumer<T> : IConsumer<T> where T : class, IIntegrationEvent
    {
        private readonly IReservationRepository _reservationRepository;
        private static readonly ConcurrentDictionary<int, SemaphoreSlim> _locks = new ConcurrentDictionary<int, SemaphoreSlim>();
        private SemaphoreSlim GetLock(int reservationId)
        {
            return _locks.GetOrAdd(reservationId, _ => new SemaphoreSlim(1, 1));
        }

        public FinishReservationConsumer(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task Consume(ConsumeContext<T> context)
        {
            if (context.Message is UserValidatedConsumerDTO userValidatedEvent)
                await HandleUserValidation(userValidatedEvent);

            if (context.Message is ResourceValidatedConsumerDTO resourceValidatedEvent)
                await HandleResourceValidation(resourceValidatedEvent);
        }

        private async Task HandleUserValidation(UserValidatedConsumerDTO userValidatedEvent)
        {
            var sem = GetLock(userValidatedEvent.ReservationId);
            await sem.WaitAsync();

            try
            {
                var reservation = await _reservationRepository.GetById(userValidatedEvent.ReservationId);
                if (reservation is null) return;

                reservation.SetUserValidation(userValidatedEvent.IsValid);
                if (reservation.JsConfig is null)
                {
                    var jsConfig = new JsConfigDTO();
                    jsConfig.SetUserValidation(userValidatedEvent.IsValid);

                    reservation.JsConfig = JsonConvert.SerializeObject(jsConfig);
                }
                else
                {
                    var jsConfig = JsonConvert.DeserializeObject<JsConfigDTO>(reservation.JsConfig);
                    jsConfig.SetUserValidation(userValidatedEvent.IsValid);

                    reservation.JsConfig = JsonConvert.SerializeObject(jsConfig);
                }

                await _reservationRepository.Update(reservation);
                await TryConfirmBooking(reservation);
            }
            finally 
            {
                sem.Release();
            }
        }

        private async Task HandleResourceValidation(ResourceValidatedConsumerDTO resourceValidatedEvent)
        {
            var sem = GetLock(resourceValidatedEvent.IdReservation);
            await sem.WaitAsync();

            try
            {
                var reservation = await _reservationRepository.GetById(resourceValidatedEvent.IdReservation);
                if (reservation is null) return;

                reservation.SetResourceValidation(resourceValidatedEvent.IsAvailable);

                if (reservation.JsConfig is null)
                {
                    var jsConfig = new JsConfigDTO();
                    jsConfig.SetResourceValidation(resourceValidatedEvent.IsAvailable);

                    reservation.JsConfig = JsonConvert.SerializeObject(jsConfig);
                }
                else
                {
                    var jsConfig = JsonConvert.DeserializeObject<JsConfigDTO>(reservation.JsConfig);
                    jsConfig.SetResourceValidation(resourceValidatedEvent.IsAvailable);

                    reservation.JsConfig = JsonConvert.SerializeObject(jsConfig);
                }

                await _reservationRepository.Update(reservation);
                await TryConfirmBooking(reservation);
            }
            finally
            {
                sem.Release();
            }
        }

        private async Task TryConfirmBooking(Reservation reservation)
        {
            var jsConfig = JsonConvert.DeserializeObject<JsConfigDTO>(reservation.JsConfig);
            if (!jsConfig.UserValidated || !jsConfig.ResourceValidated)
                return;

            if (jsConfig.UserValidated && jsConfig.ResourceValidated)
                reservation.MarkAsConfirmed();
            else
                reservation.MarkAsFailed();

            await _reservationRepository.Update(reservation);
        }
    }
}
