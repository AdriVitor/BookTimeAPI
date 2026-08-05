namespace Communication.MessageBus.Abstractions
{
    public interface IIntegrationEvent
    {
        public Guid Id { get; }
        public DateTime OcurredOn { get; }
    }
}
