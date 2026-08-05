using Communication.MessageBus.Core.Abstractions;
using Communication.MessageBus.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MessageBus.Connections;

public class RabbitMqService : IRabbitMqService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string hostNameAdress = "localhost";
    public RabbitMqService()
    {
        var factory = new ConnectionFactory()
        {
            HostName = hostNameAdress,
            UserName = "guest",
            Password = "guest",
            VirtualHost = "/"
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void ConfigureQueue(string queue, bool durable, bool exclusive, bool autoDelete, string exchange, IDictionary<string, object> arguments = null)
    {
        _channel.QueueDeclare
            (queue: queue,
             durable: durable,
             exclusive: exclusive,
             autoDelete: autoDelete,
             arguments: arguments);

        _channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct, durable: durable, autoDelete: autoDelete, arguments: arguments);
    }

    public void QueueBind(string queue, string exchange, string routingKey)
    {
        _channel.QueueBind(queue: queue, exchange: exchange, routingKey: routingKey);
    }

    public void SendMessage(string message, string exchange, string routingKey, IBasicProperties basicProperties = null)
    {
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: basicProperties, body: body);
    }

    public async Task<ReceiveMessageDTO> ReceiveMessage(string queue)
    {
        var taskCompletionSource = new TaskCompletionSource<ReceiveMessageDTO>();
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            taskCompletionSource.SetResult(new ReceiveMessageDTO(message, eventArgs));
        };
        _channel.BasicConsume(queue: queue, autoAck: true, consumer: consumer);
        return await taskCompletionSource.Task;
    }
}

