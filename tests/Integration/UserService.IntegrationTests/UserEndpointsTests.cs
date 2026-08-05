using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using UserService_Application.DTOs.User;
using UserService_Application.DTOs.Users;
using UserService_Domain.Entities;
using UserService_Infraestructure.Context;

namespace UserService.IntegrationTests;

public class UserEndpointsTests : IClassFixture<UserApiFactory>
{
    private readonly UserApiFactory _factory;

    public UserEndpointsTests(UserApiFactory factory) => _factory = factory;

    private int SeedRole(string name = "Customer")
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ContextDb>();

        var role = new Role(name);
        db.Roles.Add(role);
        db.SaveChanges();

        return role.Id;
    }

    private int SeedUser(string email, string password = "123456")
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ContextDb>();

        //Console.WriteLine(db.Database.ProviderName);

        var user = new User("Maria Silva", email, password, "12345678901", DateTime.UtcNow.AddYears(-30));
        db.Users.Add(user);
        db.SaveChanges();

        return user.Id;
    }

    [Fact]
    public async Task CreateUser_ComDadosValidos_DeveRetornarOkEPersistir()
    {
        var client = _factory.CreateClient();
        var roleId = SeedRole();

        var response = await client.PostAsJsonAsync("/api/v1/users", new
        {
            Name = "John Doe",
            Email = "john.doe@test.com",
            Password = "123456",
            CPF = "12345678901",
            DateOfBirth = DateTime.UtcNow.AddYears(-25),
            IdRole = roleId
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ContextDb>();
        Assert.Contains(db.Users, u => u.Email == "john.doe@test.com");
    }

    [Fact]
    public async Task GetUserById_SemToken_DeveRetornarUnauthorized()
    {
        var client = _factory.CreateClient();

        var response = await client.GetAsync("/api/v1/users/1");

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GetUserById_ComToken_DeveRetornarUsuario()
    {
        var client = _factory.CreateClient();
        var userId = SeedUser("get.user@test.com");

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", TestAuth.GenerateToken(userId));

        var response = await client.GetAsync($"/api/v1/users/{userId}");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var user = await response.Content.ReadFromJsonAsync<GetUserDTO>();
        Assert.NotNull(user);
        Assert.Equal("get.user@test.com", user!.Email);
    }

    [Fact]
    public async Task Exists_ComCredenciaisCorretas_DeveRetornarIdDoUsuario()
    {
        var client = _factory.CreateClient();
        var userId = SeedUser("exists.user@test.com", "senha123");

        var response = await client.PostAsJsonAsync("/api/v1/users/exists", new UserExistsRequestDTO
        {
            Email = "exists.user@test.com",
            Password = "senha123"
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<UserExistsResponseDTO>();
        Assert.NotNull(result);
        Assert.Equal(userId, result!.Id);
    }
}
