namespace Communication.MessageBus.Core.Abstractions
{
    public interface ISendMessageService
    {
        Task SendMessage<T>(T dto, string queueName);
    }
}
