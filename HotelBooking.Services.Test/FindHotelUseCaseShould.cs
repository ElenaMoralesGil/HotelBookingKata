using HotelBookingKata.Entities;
using HotelBookingKata.Repositories;
using HotelBookingKata.Services;
using NSubstitute;
using Shouldly;
namespace HotelBooking.Services.Test;

public class FindHotelUseCaseShould
{

    private FindHotelUseCase useCase;
    private HotelRepository hotelRepository;

    [SetUp]
    public void Setup()
    {
        hotelRepository = Substitute.For<HotelRepository>();
        useCase = new FindHotelUseCase(hotelRepository);
    }

    

    [Test]
    public void return_hotel_when_finding_hotel_by_id_of_an_existing_hotel()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        hotelRepository.GetById(hotel.Id).Returns(hotel);

        var result = useCase.Execute(hotel.Id);

        result.ShouldBe(hotel);
        hotelRepository.Received(1).GetById(hotel.Id);

    }
}
