using Communication.MessageBus.Configuration;
using MassTransit;
using UserService_Application.Consumers;

namespace UserService_API.Extensions
{
    public static class MessageBrokerExtensions
    {
        public static IServiceCollection AddConfigurationMessageBroker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserValidatedConsumer>();

                MessagingDefaults.ConfigureRabbitMq(x,
                    host: configuration["RabbitMq:Host"] ?? "localhost",
                    user: configuration["RabbitMq:User"] ?? "guest",
                    pass: configuration["RabbitMq:Pass"] ?? "guest",
                    configure: (cfg, context) =>
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
