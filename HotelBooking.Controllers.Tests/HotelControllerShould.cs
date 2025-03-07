using HotelBookingKata;
using HotelBookingKata.services;
using HotelBookingKata.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests;

public class HotelControllerShould
{

    private HotelController controller;
    private HotelService hotelService;
    [SetUp]
    public void Setup()
    {
  
        hotelService = Substitute.For<HotelService>();
        controller = new HotelController(hotelService);
    }

    [Test]
    public void return_createdResult_when_adding_valid_hotel()
    {
        var hotel =new {
            Id = "hotel1",
            Name= "hotel 1"
        };
        var request = new HotelController.AddHotelRequest{ Id = hotel.Id, Name= hotel.Name};

        var result = controller.AddHotel(request);

        result.ShouldBeOfType<CreatedResult>();
        var createdResult= (CreatedResult)result;
        createdResult.Location.ShouldContain(hotel.Id);
        hotelService.Received(1).AddHotel(request.Id, request.Name);
    }

    [Test]
    public void return_conflict_when_adding_duplicated_hotel()
    {
        var hotel = new
        {
            Id = "Hotel1",
            Name = "Hotel 1"
        };
        var request = new HotelController.AddHotelRequest { Id = hotel.Id, Name = hotel.Name };

        hotelService.When(x => x.AddHotel(hotel.Id, hotel.Name))
            .Do(x => throw new InvalidOperationException("Hotel already exists"));
        
       
        var result = controller.AddHotel(request);
       
        result.ShouldBeOfType<ConflictObjectResult>();
    }
}
