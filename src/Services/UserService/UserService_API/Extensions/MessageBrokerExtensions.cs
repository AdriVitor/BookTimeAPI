using Communication.MessageBus.Configuration;
using MassTransit;
using UserService_Application.Consumers;

namespace UserService_API.Extensions
{
    public static class MessageBrokerExtensions
    {
        public static IServiceCollection AddConfigurationMessageBroker(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserValidatedConsumer>();

                MessagingDefaults.ConfigureRabbitMq(x, configure: (cfg, context) =>
                {
                    cfg.ReceiveEndpoint("user-validate-queue", e =>
                    {
                        e.ConfigureConsumer<UserValidatedConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
