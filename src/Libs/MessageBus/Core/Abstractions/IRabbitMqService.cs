using Communication.MessageBus.DTOs;
using RabbitMQ.Client;

namespace Communication.MessageBus.Core.Abstractions
{
    public interface IRabbitMqService
    {
        public void ConfigureQueue(string queue, bool durable, bool exclusive, bool autoDelete, string exchange, IDictionary<string, object> arguments = null);
        public void QueueBind(string queue, string exchange, string routingKey);
        public void SendMessage(string message, string exchange, string routingKey, IBasicProperties basicProperties = null);
        public Task<ReceiveMessageDTO> ReceiveMessage(string queue);
    }
}
