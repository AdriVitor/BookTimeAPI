using Communication.MessageBus.DTOs;
using MassTransit;
using ResourceService_Infraestructure.Repositories.Interfaces;

namespace ResourceService_Application.Consumers
{
    public class ResourceValidatedConsumer : IConsumer<ResourceValidatedRequestDTO>
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public ResourceValidatedConsumer(IResourceRepository resourceRepository, ISendEndpointProvider sendEndpointProvider)
        {
            _resourceRepository = resourceRepository;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Consume(ConsumeContext<ResourceValidatedRequestDTO> context)
        {
            var resource = await _resourceRepository.GetByIdAsync(context.Message.ResourceId);

            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:finish-reservation-resourcevalidated-queue"));

            await endpoint.Send(new ResourceValidatedConsumerDTO()
            {
                IdResource = context.Message.ResourceId,
                IdReservation = context.Message.ReservationId,
                IsAvailable = resource != null
            });
        }
    }
}
