using System.Net;
using HotelBookingKata.Entities;
using HotelBookingKata.Controllers;
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

        var response = await client.PutAsJsonAsync($"/api/hotels/{hotel.id}/rooms", room);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

    }

    [Test]
    public async Task return_not_found_when_setting_room_to_non_existing_hotel()
    {
        var hotel = new
        {
            id = "Hotel1",
            name = "Hotel 1"
        };
        var room = new
        {
            roomNumber = "1",
            roomType = RoomType.Standard
        };
        var response = await client.PutAsJsonAsync($"/api/hotels/{hotel.id}/rooms", room);
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task return_hotel_when_hotel_exists()
    {
        var hotel = new
        {
            id = "Hotel1",
            name = "Hotel 1"
        };
        var room1 = new
        {
            roomNumber = "1",
            roomType = RoomType.Standard
        };
        var room2 = new
        {
            roomNumber = "2",
            roomType = RoomType.JuniorSuite
        };
        await client.PostAsJsonAsync("/api/hotels", hotel);
        await client.PutAsJsonAsync($"/api/hotels/{hotel.id}/rooms", room1);
        await client.PutAsJsonAsync($"/api/hotels/{hotel.id}/rooms", room2);

        var response = await client.GetAsync($"/api/hotels/{hotel.id}");
        var result = await response.Content.ReadFromJsonAsync<HotelResponse>();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        hotel.ShouldNotBeNull();
        result.Id.ShouldBe(hotel.id);
        result.Name.ShouldBe(hotel.name);
        result.RoomCounts.Count.ShouldBe(2);
    }

}
