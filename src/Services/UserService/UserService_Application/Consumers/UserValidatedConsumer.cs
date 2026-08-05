using Communication.MessageBus.DTOs;
using MassTransit;
using UserService_Application.Services.Interfaces;

namespace UserService_Application.Consumers
{
    public class UserValidatedConsumer : IConsumer<UserValidatedRequestDTO>
    {
        private readonly IUserService _userService;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public UserValidatedConsumer(IUserService userService, ISendEndpointProvider sendEndpointProvider)
        {
            _userService = userService;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Consume(ConsumeContext<UserValidatedRequestDTO> message)
        {
            var user = await _userService.GetByIdAsync(message.Message.UserId);
            bool isValid = user != null;

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:finish-reservation-uservalidated-queue"));

            await endpoint.Send(new UserValidatedConsumerDTO
            {
                UserId = message.Message.UserId,
                ReservationId = message.Message.ReservationId,
                IsValid = isValid
            });
        }
    }
}
