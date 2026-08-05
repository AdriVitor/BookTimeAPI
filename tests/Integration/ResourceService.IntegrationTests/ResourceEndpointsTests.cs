using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using ResourceService_Application.DTOs.Resources;
using ResourceService_Domain.Entities;
using ResourceService_Infraestructure.Context;

namespace ResourceService.IntegrationTests;

public class ResourceEndpointsTests : IClassFixture<ResourceApiFactory>
{
    private readonly ResourceApiFactory _factory;

    public ResourceEndpointsTests(ResourceApiFactory factory) => _factory = factory;

    private HttpClient CreateAuthenticatedClient()
    {
        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", TestAuth.GenerateToken());
        return client;
    }

    private int SeedUf(string name = "São Paulo", string acronym = "SP")
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ContextDb>();

        var uf = new Uf { Name = name, Acronym = acronym };
        db.Uf.Add(uf);
        db.SaveChanges();

        return uf.Id;
    }

    private int SeedResource(int idUf, string name = "Sala de Reunião")
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ContextDb>();

        var resource = new Resource(1, name, "Recurso para testes de integração", idUf, "Rua Teste, 123");
        db.Resources.Add(resource);
        db.SaveChanges();

        return resource.Id;
    }

    [Fact]
    public async Task CreateResource_SemToken_DeveRetornarUnauthorized()
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/v1/resource", new ResourceDTO
        {
            IdUser = 1,
            Name = "Quadra",
            Description = "Quadra poliesportiva coberta",
            IdUf = 1,
            Address = "Av. Central, 1000"
        });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateResource_ComToken_DeveRetornarOkEPersistir()
    {
        var client = CreateAuthenticatedClient();
        var idUf = SeedUf();

        var response = await client.PostAsJsonAsync("/api/v1/resource", new ResourceDTO
        {
            IdUser = 1,
            Name = "Auditório",
            Description = "Auditório com capacidade para 200 pessoas",
            IdUf = idUf,
            Address = "Av. Central, 1000"
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ContextDb>();
        Assert.Contains(db.Resources, r => r.Name == "Auditório");
    }

    [Fact]
    public async Task GetResourceById_ComToken_DeveRetornarRecurso()
    {
        var client = CreateAuthenticatedClient();
        var idUf = SeedUf("Rio de Janeiro", "RJ");
        var resourceId = SeedResource(idUf, "Espaço Gourmet");

        var response = await client.GetAsync($"/api/v1/resource/{resourceId}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        Assert.Equal("Espaço Gourmet", json.GetProperty("name").GetString());
    }
}
