using System.Net;
using HotelBookingKata;
using NSubstitute;
using Shouldly;
using HotelBookingKata.services;
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
        var hotel =new Hotel("hotel1","hotel 1" );

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
}
