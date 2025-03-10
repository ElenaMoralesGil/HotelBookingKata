using System.Net;
using HotelBookingKata.Entities;
using System.Net.Http.Json;
using Shouldly;

namespace HotelBookingKata.Test;

public class HotelApiShould  {
    private CustomWebApplicationFactory<Program> factory;
    private HttpClient client;

    [SetUp]
    public void Setup() {
        factory = new CustomWebApplicationFactory<Program>();
        client = factory.CreateClient();

    }

    [TearDown]
    public void TearDown() {
        client?.Dispose();
        factory?.Dispose();
    }


    [Test]
    public async Task add_hotel_when_created(){

        var hotel =  new {
            id = "Hotel1",
            name = "Hotel 1"
        };

        var response = await client.PostAsJsonAsync("/api/hotels", hotel);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ToString().ShouldContain("Hotel1");

    }
    [Test]
    public async Task return_conflict_when_adding_duplicated_hotel()
    {
        var hotel = new
        {
            id = "Hotel1",
            name = "Hotel 1"
        };

        await client.PostAsJsonAsync("/api/hotels", hotel);
        var response = await client.PostAsJsonAsync("/api/hotels", hotel);

        response.StatusCode.ShouldBe(HttpStatusCode.Conflict);

    }
    [Test]
    public async Task set_room_when_hotel_exists()
    {
        var hotel = new
        {
            id = "Hotel1",
            name = "Hotel 1"
        };
        await client.PostAsJsonAsync("/api/hotels", hotel);
        var room = new
        {
            roomNumber = "1",
            roomType = RoomType.Standard
        };

        var response = await client.PostAsJsonAsync($"/api/hotels/{hotel.id}/rooms", room);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

    }

}
