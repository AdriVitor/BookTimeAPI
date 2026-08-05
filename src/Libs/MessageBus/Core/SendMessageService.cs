using Communication.MessageBus.Core.Abstractions;
using MassTransit;

namespace Communication.MessageBus.Core
{
    public class SendMessageService : ISendMessageService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public SendMessageService(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task SendMessage<T>(T dto, string queueName)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
            await endpoint.Send(dto);
        }
    }
}
