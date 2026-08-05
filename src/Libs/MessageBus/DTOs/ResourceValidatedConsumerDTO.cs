using Communication.MessageBus.Abstractions;

namespace Communication.MessageBus.DTOs
{
    public class ResourceValidatedConsumerDTO : IIntegrationEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime OcurredOn { get; } = DateTime.Now;
        public int IdReservation { get; set; }
        public int IdResource { get; set; }
        public bool IsAvailable { get; set; }
    }
}
