using HotelBookingKata;
using HotelBookingKata.services;
using HotelBookingKata.Controllers;
using HotelBookingKata.Entities;
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
    }

    [Test]
    public void return_conflict_when_adding_duplicated_hotel()
    {
        var hotel = new
        {
            Id = "Hotel1",
            Name = "Hotel 1"
        };
        hotelService.When(x => x.AddHotel(hotel.Id, hotel.Name)).Do(x => throw new InvalidOperationException("Hotel already exists"));
        var request = new HotelController.AddHotelRequest { Id = hotel.Id, Name = hotel.Name };
        var result = controller.AddHotel(request);
        result.ShouldBeOfType<ConflictObjectResult>();
        var conflictResult = (ConflictObjectResult)result;
    }

    [Test]
    public void return_noContent_when_setting_a_valid_room_to_an_existing_hotel()
    {
        var hotel = new
        {
            Id = "Hotel1",
            Name = "Hotel 1"
        };
        var request = new HotelController.SetRoomRequest
        {
            RoomNumber = "1",
            RoomType = RoomType.Standard
        };

        var result = controller.SetRoom(hotel.Id, request);

        result.ShouldBeOfType<NoContentResult>();
        hotelService.Received(1).SetRoom(hotel.Id, request.RoomNumber, request.RoomType);
    }

    [Test]
    public void return_notFound_when_setting_room_to_non_existing_hotel()
    {
        var hotel = new
        {
            Id = "Hotel1",
            Name = "Hotel 1"
        };
        hotelService.When(x => x.SetRoom(hotel.Id, "1", RoomType.Standard)).Do(x => throw new InvalidOperationException("Hotel does not exist"));
        var request = new HotelController.SetRoomRequest
        {
            RoomNumber = "1",
            RoomType = RoomType.Standard
        };
        var result = controller.SetRoom(hotel.Id, request);
        result.ShouldBeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
    }
}
