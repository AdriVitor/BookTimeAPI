using BookingService_Application.Consumers;
using Communication.MessageBus.Configuration;
using Communication.MessageBus.Core;
using Communication.MessageBus.Core.Abstractions;
using Communication.MessageBus.DTOs;
using Communication.MessageBus.Extensions;
using MassTransit;

namespace BookingService_API.Extensions
{
    public static class MessageBrokerExtensions
    {
        public static IServiceCollection AddConfigurationMessaging(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<FinishReservationConsumer<UserValidatedConsumerDTO>>();
                x.AddConsumer<FinishReservationConsumer<ResourceValidatedConsumerDTO>>();

                MessagingDefaults.ConfigureRabbitMq(x, configure: (cfg, context) =>
                {
                    cfg.ReceiveEndpoint("finish-reservation-uservalidated-queue", e =>
                    {
                        e.ConfigureConsumer<FinishReservationConsumer<UserValidatedConsumerDTO>>(context);
                    });

                    cfg.ReceiveEndpoint("finish-reservation-resourcevalidated-queue", e =>
                    {
                        e.ConfigureConsumer<FinishReservationConsumer<ResourceValidatedConsumerDTO>>(context);
                    });
                });
            });

            services.AddScoped<ISendMessageService, SendMessageService>();

            return services;
        }
    }
}
