using HotelBookingKata.Entities;
using HotelBookingKata.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using HotelBookingKata.FindHotel;

namespace HotelBooking.Controllers.Tests;

public class FindHotelControllerShould
{

    private FindHotelController controller;
    private FindHotelUseCase useCase;

    [SetUp]
    public void Setup()
    {
        useCase = Substitute.For<FindHotelUseCase>();
        controller = new FindHotelController(useCase);
    }

    [Test]
    public void return_Ok_when_getting_existing_hotel()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        hotel.SetRoom("1", RoomType.Standard);
        useCase.Execute(hotel.Id).Returns(hotel);

        var result = controller.GetHotel(hotel.Id);

        result.ShouldBeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        var hotelResponse = okResult.Value as FindHotelResponse;
        hotelResponse.Id.ShouldBe(hotel.Id);
        hotelResponse.Name.ShouldBe(hotel.Name);
        hotelResponse.Rooms[0].Type.ShouldBe(RoomType.Standard);
        hotelResponse.Rooms[0].Number.ShouldBe("1");
    }
}
