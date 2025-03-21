using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using HotelBookingKata.SetRoom;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Services.Test;

class SetRoomUseCaseShould
{
    private HotelRepository hotelRepository;
    private SetRoomUseCase useCase;

    [SetUp]
    public void Setup()
    {
        hotelRepository = Substitute.For<HotelRepository>();
        useCase = new SetRoomUseCase(hotelRepository);
    }
    [Test]
    public void set_room_when_hotel_exists()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        var room = new Room(RoomType.Standard, "1");
        hotelRepository.Exists(hotel.Id).Returns(true);
        hotelRepository.GetById(hotel.Id).Returns(hotel);

        useCase.Execute(hotel.Id, room.Number, room.Type);
        hotelRepository.Received(1).Update(hotel);
    }

    [Test]
    public void return_not_found_when_hotel_doesnt_exist()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        var room = new Room(RoomType.Standard, "1");
        hotelRepository.GetById(hotel.Id).Returns((Hotel)null);
        Should.Throw<HotelNotFoundException>(() =>
        useCase.Execute(hotel.Id, room.Number, room.Type))
            .Message.ShouldBe("Hotel with id hotel1 not found");
        hotelRepository.DidNotReceive().Update(Arg.Any<Hotel>());
    }
}
