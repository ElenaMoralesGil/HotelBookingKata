using System.Net;
using HotelBookingKata;
using NSubstitute;
namespace HotelBooking.Services.Test;

public class HotelServiceShould
{

    private HotelService hotelService;
    private HotelRepository hotelRepository;

    [SetUp]
    public void Setup()
    {
        hotelRepository = Substitute.For<HotelRepository>();
        hotelService = new HotelService(hotelRepository);
    }

    [Test]
    public void add_hotel_when_hotel_doesnt_exist()
    {
        var hotel =new Hotel("hotel1","hotel 1" );

        hotelRepository.Add(hotel);

        hotelRepository.Received(1).Add(Arg.Is<Hotel>(h => h.Id == hotel.Id && h.Name == hotel.Name));
    }
}
