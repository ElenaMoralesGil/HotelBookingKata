using HotelBookingKata;
using HotelBookingKata.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests;

public class HotelControllerShould
{

    private HotelController controllers;
    private HotelService hotelService;
    [SetUp]
    public void Setup()
    {
        hotelService = Substitute.For<HotelService>(Substitute.For<HotelRepository>());
        controllers = new HotelController(hotelService);
    }

    [Test]
    public void return_createdResult_when_adding_valid_hotel()
    {
        var hotel =new {
            Id = "hotel1",
            Name= "hotel 1"
        };
        var request = new HotelController.AddHotelRequest{ Id = hotel.Id, Name= hotel.Name};

        var result = controllers.AddHotel(request);

        result.ShouldBeOfType<CreatedResult>();
        var createdResult= (CreatedResult)result;
        createdResult.Location.ShouldContain(hotel.Id);
    }
}
