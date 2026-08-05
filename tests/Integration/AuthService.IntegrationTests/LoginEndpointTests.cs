using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace AuthService.IntegrationTests;

public class LoginEndpointTests : IClassFixture<AuthApiFactory>
{
    private readonly AuthApiFactory _factory;

    public LoginEndpointTests(AuthApiFactory factory) => _factory = factory;

    [Fact]
    public async Task Login_ComCredenciaisValidas_DeveRetornarToken()
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/v1/login", new
        {
            Email = "user@test.com",
            Password = "123456"
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var json = await response.Content.ReadFromJsonAsync<JsonElement>();
        var token = json.GetProperty("token").GetString();
        Assert.False(string.IsNullOrWhiteSpace(token));
    }
}
