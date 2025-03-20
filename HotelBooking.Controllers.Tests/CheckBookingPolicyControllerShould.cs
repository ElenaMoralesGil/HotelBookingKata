using HotelBookingKata.CheckBookingPolicy;
using HotelBookingKata.Entities;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests;
class CheckBookingPolicyControllerShould
{
    private CheckBookingPolicyController controller;
    private CheckBookingPolicyUseCase useCase;

    [SetUp]
    public void Setup()
    {
        useCase = Substitute.For<CheckBookingPolicyUseCase>();
        controller = new CheckBookingPolicyController(useCase);
    }


    [Test]
    public void returns_ok__with_true_when_policy_is_allowed()
    {
        var employeeId = "Employee1";
        var roomType = RoomType.Standard;
        useCase.Execute(employeeId, roomType).Returns(true);

        var result = controller.IsBookingAllowed(employeeId, roomType);

        result.ShouldBeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        okResult.Value.ShouldBe(true);
        useCase.Received(1).Execute(employeeId, roomType);
    }

    [Test]
    public void returns_ok__with_false_when_policy_is_not_allowed()
    {
        var employeeId = "Employee1";
        var roomType = RoomType.Standard;
        useCase.Execute(employeeId, roomType).Returns(false);

        var result = controller.IsBookingAllowed(employeeId, roomType);

        result.ShouldBeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        okResult.Value.ShouldBe(false);
        useCase.Received(1).Execute(employeeId, roomType);
    }
}



