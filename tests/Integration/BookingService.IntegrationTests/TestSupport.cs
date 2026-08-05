using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Communication.MessageBus.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace BookingService.IntegrationTests;

public static class TestAuth
{
    public const string SecretKey = "integration-tests-secret-key-0123456789-abcdefghij";
    public const string Issuer = "booktime";
    public const string Audience = "clients";

    public static Dictionary<string, string?> JwtConfig() => new()
    {
        ["JwtSettings:SecretKey"] = SecretKey,
        ["JwtSettings:Issuer"] = Issuer,
        ["JwtSettings:Audience"] = Audience,
        ["JwtSettings:ExpirationMinutes"] = "60",
    };

    public static string GenerateToken(int customerId = 1)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, customerId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(Issuer, Audience, claims,
            expires: DateTime.UtcNow.AddMinutes(60), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

/// <summary>Substitui o envio de mensagens (RabbitMQ) por um no-op nos testes.</summary>
public class FakeSendMessageService : ISendMessageService
{
    public Task SendMessage<T>(T dto, string queueName) => Task.CompletedTask;
}

public static class TestServiceCollectionExtensions
{
    public static void ReplaceDbContextWithInMemory<TContext>(this IServiceCollection services, string dbName)
        where TContext : DbContext
    {
        // Remove o registro do DbContext
        services.RemoveAll<TContext>();

        // Remove as op��es do DbContext
        services.RemoveAll<DbContextOptions<TContext>>();
        services.RemoveAll(typeof(DbContextOptions));

        // Remove configura��es adicionais do EF Core (importante no .NET 8)
        services.RemoveAll<IDbContextOptionsConfiguration<TContext>>();

        // Registra novamente usando InMemory
        services.AddDbContext<TContext>(options =>
        {
            options.UseInMemoryDatabase(dbName);
        });
    }

    public static void RemoveMassTransitHostedService(this IServiceCollection services)
    {
        var descriptors = services
            .Where(d => d.ServiceType == typeof(IHostedService)
                        && d.ImplementationType?.FullName?.Contains("MassTransit") == true)
            .ToList();

        foreach (var descriptor in descriptors)
            services.Remove(descriptor);
    }
}
