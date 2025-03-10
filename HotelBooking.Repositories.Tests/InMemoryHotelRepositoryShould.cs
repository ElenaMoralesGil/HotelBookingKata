using System.Net;
using HotelBookingKata.Repositories;
using HotelBookingKata.Entities;
using NSubstitute;
using Shouldly;
namespace HotelBooking.Repositories.Tests;

public class InMemoryHotelRepositoryShould
{
    private InMemoryHotelRepository repository;

    [SetUp]
    public void Setup()
    {
        repository = new InMemoryHotelRepository();
    }

    [Test]
    public void add_hotel_to_existing_hotels()
    {
        var hotel = new Hotel("hotel1", "hotel 1");

        repository.Add(hotel);

        repository.hotels.Count.ShouldBe(1);
        repository.hotels.ShouldContainKey("hotel1");
    }

    [Test]
    public void get_id_from_existing_hotel()
    {
        var hotel = new Hotel("hotel1", "hotel 1");

        repository.Add(hotel);
        var result = repository.GetById(hotel.Id);

        result.ShouldBe(hotel);
    }

    [Test]
    public void return_true_when_hotel_exists()
    {
        var hotel = new Hotel("hotel1", "hotel 1");

        repository.Add(hotel);
        var result = repository.Exists(hotel.Id);

        result.ShouldBe(true);

    }

    [Test]
    public void return_updated_hotel_when_setting_room()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        var room = new Room("1", RoomType.Standard);
        repository.Add(hotel);
        hotel.SetRoom(room.Number, room.Type);

        repository.Update(hotel);
        var result = repository.GetById(hotel.Id);

        result.Rooms.Count.ShouldBe(1);
        result.Rooms.ShouldContainKey(room.Number);
    }
}
