using HotelBookingKata.Controllers;
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.AddHotel;
using HotelBookingKata.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests;

public class HotelsControllerShould
{

    private HotelsController controller;
    private HotelService hotelService;

    [SetUp]
    public void Setup()
    {
        hotelService = Substitute.For<HotelService>();
        controller = new HotelsController(hotelService);
    }

    [Test]
    public void return_Ok_when_getting_existing_hotel()
    {
        var hotel = new Hotel("hotel1", "hotel 1");
        hotel.SetRoom("1", RoomType.Standard);
        hotelService.FindHotelBy(hotel.Id).Returns(hotel);

        var result = controller.GetHotel(hotel.Id);

        result.ShouldBeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        var hotelResponse = okResult.Value as HotelResponse;
        hotelResponse.Id.ShouldBe(hotel.Id);
        hotelResponse.Name.ShouldBe(hotel.Name);
        hotelResponse.Rooms[0].Type.ShouldBe(RoomType.Standard);
        hotelResponse.Rooms[0].Number.ShouldBe("1");
    }
}
