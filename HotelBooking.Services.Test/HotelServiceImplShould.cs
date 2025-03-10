using System.Net;
using NSubstitute;
using Shouldly;
using HotelBookingKata.services;
using HotelBookingKata.Entities;
using HotelBookingKata.Repositories;
namespace HotelBooking.Services.Test;

public class HotelServiceImplShould
{

    private HotelServiceImpl hotelService;
    private HotelRepository hotelRepository;

    [SetUp]
    public void Setup()
    {
        hotelRepository = Substitute.For<HotelRepository>();
        hotelService = new HotelServiceImpl(hotelRepository);
    }

    [Test]
    public void add_hotel_when_hotel_doesnt_exist()
    {
        var hotel = new Hotel("hotel1", "hotel 1");

        hotelRepository.Add(hotel);

        hotelRepository.Received(1).Add(Arg.Is<Hotel>(h => h.Id == hotel.Id && h.Name == hotel.Name));
    }

    [Test]
    public void return_conflict_when_hotel_exists()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        hotelRepository.Exists(hotel.Id).Returns(true);

        Should.Throw<InvalidOperationException>(() =>
        hotelService.AddHotel(hotel.Id, hotel.Name))
            .Message.ShouldBe("Hotel already exists");
        hotelRepository.Received(1).Exists(hotel.Id);
    }

    [Test]
    public void set_room_when_hotel_exists()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        var room = new Room("1", RoomType.Standard);
        hotelRepository.GetById(hotel.Id).Returns(hotel);

        hotelService.SetRoom(hotel.Id, room.Number, room.Type);
        hotelRepository.Received(1).Update(hotel);
    }

    [Test]
    public void return_not_found_when_hotel_doesnt_exist()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        var room = new Room("1", RoomType.Standard);
        hotelRepository.GetById(hotel.Id).Returns((Hotel) null);
        Should.Throw<InvalidOperationException>(() =>
        hotelService.SetRoom(hotel.Id, room.Number, room.Type))
            .Message.ShouldBe("Hotel does not exist");
        hotelRepository.Received(1).GetById(hotel.Id);
        hotelRepository.DidNotReceive().Update(Arg.Any<Hotel>());
    }

    [Test]
    public void return_hotel_when_finding_hotel_by_id_of_an_existing_hotel()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        hotelRepository.GetById(hotel.Id).Returns(hotel);

        var result = hotelService.FindHotelBy(hotel.Id);

        result.ShouldBe(hotel);
        hotelRepository.Received(1).GetById(hotel.Id);

    }
}
