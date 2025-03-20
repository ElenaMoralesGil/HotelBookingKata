using HotelBookingKata.AddHotel;
using HotelBookingKata.Exceptions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests;

class AddHotelControllerShould
{
    private AddHotelUseCase useCase;
    private AddHotelController controller;

    [SetUp]
    public void Setup()
    {
        useCase = Substitute.For<AddHotelUseCase>();
        controller = new AddHotelController(useCase);
    }
    
    [Test]
    public void return_createdResult_when_adding_valid_hotel()
    {
        var hotel = new
        {
            Id = "hotel1",
            Name = "hotel 1"
        };

        var request = new AddHotelRequest(hotel.Id, hotel.Name);

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
        var request = new AddHotelRequest(hotel.Id, hotel.Name);
        useCase.When(x => x.Execute(request)).Do(x => throw new HotelAlreadyExistsException(hotel.Id));
        var result = controller.AddHotel(request);
        result.ShouldBeOfType<ConflictObjectResult>();
        var conflictResult = (ConflictObjectResult)result;
    }
}
