using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using HotelBookingKata.Services;
using NSubstitute;
using Shouldly;
namespace HotelBooking.Services.Test;

public class ApplicationHotelServiceShould
{

    private AppHotelService hotelService;
    private HotelRepository hotelRepository;

    [SetUp]
    public void Setup()
    {
        hotelRepository = Substitute.For<HotelRepository>();
        hotelService = new AppHotelService(hotelRepository);
    }

    [Test]
    public void set_room_when_hotel_exists()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        var room = new Room(RoomType.Standard, "1");
        hotelRepository.Exists(hotel.Id).Returns(true);
        hotelRepository.GetById(hotel.Id).Returns(hotel);

        hotelService.SetRoom(hotel.Id, room.Number, room.Type);
        hotelRepository.Received(1).Update(hotel);
    }

    [Test]
    public void return_not_found_when_hotel_doesnt_exist()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        var room = new Room(RoomType.Standard, "1");
        hotelRepository.GetById(hotel.Id).Returns((Hotel)null);
        Should.Throw<HotelNotFoundException>(() =>
        hotelService.SetRoom(hotel.Id, room.Number, room.Type))
            .Message.ShouldBe("Hotel with id hotel1 not found");
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
