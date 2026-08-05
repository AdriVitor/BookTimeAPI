using MassTransit.RabbitMqTransport;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Communication.MessageBus.Configuration;

namespace Communication.MessageBus.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //public static IServiceCollection AddMessaging(this IServiceCollection services,
        //                                              Action<IRabbitMqBusFactoryConfigurator, 
        //                                              IBusRegistrationContext> configureEndpoints)
        //{
        //    //return services.AddDefaultMassTransit(configureEndpoints);
        //}
    }
}
