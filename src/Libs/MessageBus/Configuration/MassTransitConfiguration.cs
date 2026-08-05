using Communication.MessageBus.DTOs;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;

namespace Communication.MessageBus.Configuration
{
    public static class MessagingDefaults
    {
        public static void ConfigureRabbitMq(
            IBusRegistrationConfigurator x,
            string host = "localhost",
            string user = "guest",
            string pass = "guest",
            Action<IRabbitMqBusFactoryConfigurator, IBusRegistrationContext>? configure = null)
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(host, "/", h =>
                {
                    h.Username(user);
                    h.Password(pass);
                });

                configure?.Invoke(cfg, context);
            });
        }
    }

}
