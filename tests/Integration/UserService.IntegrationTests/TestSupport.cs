using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace UserService.IntegrationTests;

/// <summary>
/// Gera tokens JWT válidos para os testes usando a mesma chave injetada na API de teste.
/// </summary>
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

    public static string GenerateToken(int userId = 1)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(Issuer, Audience, claims,
            expires: DateTime.UtcNow.AddMinutes(60), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public static class TestServiceCollectionExtensions
{
    /// <summary>Troca o provedor do EF Core (PostgreSQL) por um banco em memória.</summary>
    public static void ReplaceDbContextWithInMemory<TContext>(this IServiceCollection services, string dbName)
        where TContext : DbContext
    {
        // Remove o registro do DbContext
        services.RemoveAll<TContext>();

        // Remove as opções do DbContext
        services.RemoveAll<DbContextOptions<TContext>>();
        services.RemoveAll(typeof(DbContextOptions));

        // Remove configurações adicionais do EF Core (importante no .NET 8)
        services.RemoveAll<IDbContextOptionsConfiguration<TContext>>();

        // Registra novamente usando InMemory
        services.AddDbContext<TContext>(options =>
        {
            options.UseInMemoryDatabase(dbName);
        });
    }

    /// <summary>Remove o hosted service do MassTransit para não depender do RabbitMQ nos testes.</summary>
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
