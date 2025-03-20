using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using HotelBookingKata.Services;
using NSubstitute;
using Shouldly;
using HotelBookingKata.AddHotel;

namespace HotelBooking.Services.Test;

class AddHotelUseCaseShould
{
    private AddHotelUseCase useCase;
    private HotelRepository hotelRepository;

    [SetUp]
    public void Setup()
    {
        hotelRepository = Substitute.For<HotelRepository>();
        useCase = new AddHotelUseCase(hotelRepository);
    }

    [Test]
    public void add_hotel_when_hotel_doesnt_exist()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        var request = new AddHotelRequest(hotel.Id, hotel.Name);

        useCase.Execute(request);

        hotelRepository.Received(1).Add(Arg.Is<Hotel>(h => h.Id == hotel.Id && h.Name == hotel.Name));
    }

    [Test]
    public void return_conflict_when_hotel_exists()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        hotelRepository.Exists(hotel.Id).Returns(true);
        var request = new AddHotelRequest(hotel.Id, hotel.Name);

        Should.Throw<HotelAlreadyExistsException>(() =>
        useCase.Execute(request))
            .Message.ShouldBe($"Hotel with id {hotel.Id} already exists");

        hotelRepository.Received(1).Exists(hotel.Id);
    }
}
