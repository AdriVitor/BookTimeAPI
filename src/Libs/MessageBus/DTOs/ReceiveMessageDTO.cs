using Communication.MessageBus.Abstractions;
using RabbitMQ.Client.Events;

namespace Communication.MessageBus.DTOs
{
    public class ReceiveMessageDTO : IIntegrationEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime OcurredOn { get; } = DateTime.Now;
        public string Message { get; set; }
        public BasicDeliverEventArgs EventArgs { get; set; }

        public ReceiveMessageDTO(string message, BasicDeliverEventArgs eventArgs)
        {
            Message = message;
            EventArgs = eventArgs;
        }
    }
}
