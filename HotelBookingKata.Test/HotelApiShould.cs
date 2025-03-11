using System.Net;
using HotelBookingKata.Entities;
using HotelBookingKata.Controllers;
using System.Net.Http.Json;
using Shouldly;
using System.Text.Json;

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

        var repository = factory.GetRepository();
        repository.Exists(hotel.id).ShouldBeTrue();
        var storedHotel = repository.GetById(hotel.id);
        storedHotel.Id.ShouldBe(hotel.id);
        storedHotel.Name.ShouldBe(hotel.name);

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

        var repository = factory.GetRepository();
        repository.Exists(hotel.id).ShouldBeTrue();
        repository.hotels.Count.ShouldBe(1);

    }
    [Test]
    public async Task set_room_when_hotel_exists()
    {
        var hotel = new
        {
            Id = "Hotel1",
            Name = "Hotel 1"
        };
        await client.PostAsJsonAsync("/api/hotels", hotel);
        var room = new
        {
            Number = "1",
            Type = RoomType.Standard
        };

        var response = await client.PutAsJsonAsync($"/api/hotels/{hotel.Id}/rooms/{room.Type}", room);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        var repository = factory.GetRepository();
        var storedHotel = repository.GetById(hotel.Id);
        storedHotel.Rooms.Count.ShouldBe(1);
        storedHotel.Rooms[0].Number.ShouldBe("1");
        storedHotel.Rooms[0].Type.ShouldBe(RoomType.Standard);

    }

    [Test]
    public async Task return_not_found_when_setting_room_to_non_existing_hotel()
    {
        var hotel = new
        {
            Id = "Hotel1",
            Name = "Hotel 1"
        };
        var room = new
        {
            Number = "1",
            Type = RoomType.Standard
        };
        var response = await client.PutAsJsonAsync($"/api/hotels/{hotel.Id}/rooms/{room.Type}", room);
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Test]
    public async Task return_hotel_when_hotel_exists()
    {
        var hotel = new
        {
            Id = "Hotel1",
            Name = "Hotel 1",
        };
        var room1 = new
        {
            Number = "1",
            Type = RoomType.Standard

        };
        var room2 = new
        {
            Number = "2",
            Type = RoomType.JuniorSuite
        };
        await client.PostAsJsonAsync("/api/hotels", hotel);
        await client.PutAsJsonAsync($"/api/hotels/{hotel.Id}/rooms/{room1.Type}", room1);
        await client.PutAsJsonAsync($"/api/hotels/{hotel.Id}/rooms/{room2.Type}", room2);

        var response = await client.GetAsync($"/api/hotels/{hotel.Id}");
        var result = await response.Content.ReadFromJsonAsync<HotelResponse>();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        hotel.ShouldNotBeNull();
        result.ShouldNotBeNull();
        result.Id.ShouldBe(hotel.Id);
        result.Name.ShouldBe(hotel.Name);
        result.Rooms[0].Number.ShouldBe("1");
        result.Rooms[0].Type.ShouldBe(RoomType.Standard);
        result.Rooms[1].Number.ShouldBe("2");
        result.Rooms[1].Type.ShouldBe(RoomType.JuniorSuite);
    }

}
