using Communication.MessageBus.Configuration;
using MassTransit;
using ResourceService_Application.Consumers;

namespace ResourceService_API.Extensions
{
    public static class MessageBrokerExtensions
    {
        public static IServiceCollection AddConfigurationMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ResourceValidatedConsumer>();

                MessagingDefaults.ConfigureRabbitMq(x,
                    host: configuration["RabbitMq:Host"] ?? "localhost",
                    user: configuration["RabbitMq:User"] ?? "guest",
                    pass: configuration["RabbitMq:Pass"] ?? "guest",
                    configure: (cfg, context) =>
                {
                    cfg.ReceiveEndpoint("resource-validate-queue", e =>
                    {
                        e.ConfigureConsumer<ResourceValidatedConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
