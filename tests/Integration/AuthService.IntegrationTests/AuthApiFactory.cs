using AuthService_API.DTOs;
using Communication.Http.Core.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AuthService.IntegrationTests;

public class AuthApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            // O AuthService valida o usuário chamando o UserService via HTTP.
            // Nos testes, trocamos essa chamada por um fake que retorna um usuário válido.
            services.RemoveAll<IHttpClientService>();
            services.AddScoped<IHttpClientService, FakeHttpClientService>();
        });
    }
}

/// <summary>Simula o UserService retornando um usuário existente (Id = 1).</summary>
public class FakeHttpClientService : IHttpClientService
{
    public Task<TResponse> Post<TRequest, TResponse>(TRequest request, string url)
    {
        object response = new UserExistsResponseDTO { Id = 1 };
        return Task.FromResult((TResponse)response);
    }
}
