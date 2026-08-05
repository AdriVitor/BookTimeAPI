using BookingService_Infra.Context;
using Communication.MessageBus.Core.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BookingService.IntegrationTests;

public class BookingApiFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = $"BookingServiceTests-{Guid.NewGuid()}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, config) =>
        {
            config.AddInMemoryCollection(TestAuth.JwtConfig());
        });

        builder.ConfigureTestServices(services =>
        {
            services.ReplaceDbContextWithInMemory<ContextDb>(_dbName);
            services.RemoveMassTransitHostedService();

            services.RemoveAll<ISendMessageService>();
            services.AddScoped<ISendMessageService, FakeSendMessageService>();
        });
    }
}
