using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UserService_Infraestructure.Context;

namespace UserService.IntegrationTests;

/// <summary>
/// Sobe a API do UserService em memória, trocando PostgreSQL, RabbitMQ e o envio de e-mail
/// por implementações de teste, para que os testes rodem sem infraestrutura externa.
/// </summary>
public class UserApiFactory : WebApplicationFactory<Program>
{
    private readonly string _dbName = $"UserServiceTests-{Guid.NewGuid()}";

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