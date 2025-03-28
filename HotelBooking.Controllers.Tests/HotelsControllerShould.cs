﻿using HotelBookingKata.Controllers;
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
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
    public void return_createdResult_when_adding_valid_hotel()
    {
        var hotel = new
        {
            Id = "hotel1",
            Name = "hotel 1"
        };
       
        var request = new AddHotelRequest( hotel.Id,hotel.Name);

        var result = controller.AddHotel(request);

        result.ShouldBeOfType<CreatedResult>();
        var createdResult = (CreatedResult)result;
        createdResult.Location?.ShouldContain(hotel.Id);
    }

    [Test]
    public void return_conflict_when_adding_duplicated_hotel()
    {
        var hotel = new
        {
            Id = "Hotel1",
            Name = "Hotel 1"
        };
        hotelService.When(x => x.AddHotel(hotel.Id, hotel.Name)).Do(x => throw new HotelAlreadyExistsException(hotel.Id));
        var request = new AddHotelRequest(hotel.Id, hotel.Name);
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
        var request = new SetRoomNumberRequest("1");

        var result = controller.SetRoom(hotel.Id, RoomType.Standard, request);

        result.ShouldBeOfType<NoContentResult>();
        hotelService.Received(1).SetRoom(hotel.Id, request.Number, RoomType.Standard);
    }

    [Test]
    public void return_notFound_when_setting_room_to_non_existing_hotel()
    {
        var hotel = new
        {
            Id = "Hotel1",
            Name = "Hotel 1"
        };
        hotelService.When(x => x.SetRoom(hotel.Id, "1", RoomType.Standard)).Do(x => throw new HotelNotFoundException(hotel.Id));
        var request = new SetRoomNumberRequest("1");
        var result = controller.SetRoom(hotel.Id, RoomType.Standard, request);
        result.ShouldBeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
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
