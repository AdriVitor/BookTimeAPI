using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BookingService_Domain.Entities.Enums;
using BookingService_Infra.Context;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.IntegrationTests;

public class ReservationEndpointsTests : IClassFixture<BookingApiFactory>
{
    private readonly BookingApiFactory _factory;

    public ReservationEndpointsTests(BookingApiFactory factory) => _factory = factory;

    [Fact]
    public async Task CreateReservation_SemToken_DeveRetornarUnauthorized()
    {
        var client = _factory.CreateClient();

        var response = await client.PostAsJsonAsync("/api/v1/reservation", new
        {
            IdResource = 1,
            StartDate = DateTime.UtcNow.Date,
            EndDate = DateTime.UtcNow.Date.AddDays(2),
            Observation = "Reserva de teste"
        });

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task CreateReservation_ComToken_DeveCriarReservaPendente()
    {
        const int customerId = 42;

        var client = _factory.CreateClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", TestAuth.GenerateToken(customerId));

        var response = await client.PostAsJsonAsync("/api/v1/reservation", new
        {
            IdResource = 10,
            StartDate = DateTime.UtcNow.Date,
            EndDate = DateTime.UtcNow.Date.AddDays(2),
            Observation = "Reserva de teste"
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ContextDb>();

        var reservation = Assert.Single(db.Reservations);
        Assert.Equal(customerId, reservation.IdCustomer);
        Assert.Equal(10, reservation.IdResource);
        Assert.Equal((int)StatusReservationEnum.Pending, reservation.Status);
    }
}
