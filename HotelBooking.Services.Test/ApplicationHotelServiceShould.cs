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
    public void return_hotel_when_finding_hotel_by_id_of_an_existing_hotel()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        hotelRepository.GetById(hotel.Id).Returns(hotel);

        var result = hotelService.FindHotelBy(hotel.Id);

        result.ShouldBe(hotel);
        hotelRepository.Received(1).GetById(hotel.Id);

    }
}
