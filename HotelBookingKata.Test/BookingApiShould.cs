using System.Net;
using HotelBookingKata.Entities;
using System.Net.Http.Json;
using Shouldly;


namespace HotelBookingKata.Test;

class BookingApiShould
{
    private CustomWebApplicationFactory<Program> factory;
    private HttpClient client;

    [SetUp]
    public void Setup()
    {
        factory = new CustomWebApplicationFactory<Program>();
        client = factory.CreateClient();

    }

    [TearDown]
    public void TearDown()
    {
        client?.Dispose();
        factory?.Dispose();
    }

    [Test]
    public async Task create_booking_when_all_conditions_are_met()
    {
        var company = new { id = "Company1", name = "Company 1" };
        await client.PostAsJsonAsync("/api/companies", company);

        var hotel = new { id = "Hotel1", name = "Hotel 1" };
        await client.PostAsJsonAsync("/api/hotels", hotel);

        var room = new { id = "101", Type = RoomType.Standard };
        await client.PutAsJsonAsync($"/api/hotels/{hotel.id}/rooms/{room.Type}", room);

        var employee = new { EmployeeId = "Employee1" };
        await client.PostAsJsonAsync($"/api/companies/{company.id}/employees", employee);
        await client.PutAsJsonAsync($"/api/booking-policies/employees/{employee.EmployeeId}",
            new { RoomType = new[] { RoomType.Standard } });

        var booking = new
        {
            EmployeeId = employee.EmployeeId,
            HotelId = hotel.id,
            RoomType = RoomType.Standard,
            CheckIn = DateTime.Now,
            CheckOut = DateTime.Now.AddDays(1)
        };

        var response = await client.PostAsJsonAsync("/api/bookings", booking);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ToString().ShouldContain($"/api/bookings/{booking.EmployeeId}");
        var repository = factory.GetBookingRepository();
        repository.GetBookings().ShouldContainKey(booking.EmployeeId);
    }


}

