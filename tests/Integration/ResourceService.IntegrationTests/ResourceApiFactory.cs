using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourceService_Infraestructure.Context;

namespace ResourceService.IntegrationTests;

public class ResourceApiFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = $"ResourceServiceTests-{Guid.NewGuid()}";

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
        });
    }
}
