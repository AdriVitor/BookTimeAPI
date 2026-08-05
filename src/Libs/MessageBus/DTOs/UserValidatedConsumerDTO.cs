using Communication.MessageBus.Abstractions;

namespace Communication.MessageBus.DTOs
{
    public class UserValidatedConsumerDTO : IIntegrationEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime OcurredOn { get; } = DateTime.Now;
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public bool IsValid { get; set; }
    }
}
