using Communication.MessageBus.Configuration;
using MassTransit;
using ResourceService_Application.Consumers;

namespace ResourceService_API.Extensions
{
    public static class MessageBrokerExtensions
    {
        public static IServiceCollection AddConfigurationMessageBroker(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ResourceValidatedConsumer>();

                MessagingDefaults.ConfigureRabbitMq(x, configure: (cfg, context) =>
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
