using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.SetRoom;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests;

class SetRoomControllerShould
{
    private SetRoomController controller;
    private SetRoomUseCase useCase;

    [SetUp]
    public void Setup()
    {
        useCase = Substitute.For<SetRoomUseCase>();
        controller = new SetRoomController(useCase);
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
        useCase.Received(1).Execute(hotel.Id, request.Number, RoomType.Standard);
    }

    [Test]
    public void return_notFound_when_setting_room_to_non_existing_hotel()
    {
        var hotel = new
        {
            Id = "Hotel1",
            Name = "Hotel 1"
        };
        useCase.When(x => x.Execute(hotel.Id, "1", RoomType.Standard)).Do(x => throw new HotelNotFoundException(hotel.Id));
        var request = new SetRoomNumberRequest("1");
        var result = controller.SetRoom(hotel.Id, RoomType.Standard, request);
        result.ShouldBeOfType<NotFoundObjectResult>();
        var notFoundResult = (NotFoundObjectResult)result;
    }

}
