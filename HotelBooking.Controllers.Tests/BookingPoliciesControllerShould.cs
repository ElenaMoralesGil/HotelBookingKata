using HotelBookingKata.Controllers;
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests;
class BookingPoliciesControllerShould
{
    private BookingPoliciesController controller;
    private BookingPolicyService bookingPolicyService;

    [SetUp]
    public void Setup()
    {
        bookingPolicyService = Substitute.For<BookingPolicyService>();
        controller = new BookingPoliciesController(bookingPolicyService);
    }


    [Test]
    public void returns_ok__with_true_when_policy_is_allowed()
    {
        var employeeId = "Employee1";
        var roomType = RoomType.Standard;
        bookingPolicyService.IsBookingAllowed(employeeId, roomType).Returns(true);

        var result = controller.IsBookingAllowed(employeeId, roomType);

        result.ShouldBeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        okResult.Value.ShouldBe(true);
        bookingPolicyService.Received(1).IsBookingAllowed(employeeId, roomType);
    }

    [Test]
    public void returns_ok__with_false_when_policy_is_not_allowed()
    {
        var employeeId = "Employee1";
        var roomType = RoomType.Standard;
        bookingPolicyService.IsBookingAllowed(employeeId, roomType).Returns(false);

        var result = controller.IsBookingAllowed(employeeId, roomType);

        result.ShouldBeOfType<OkObjectResult>();
        var okResult = (OkObjectResult)result;
        okResult.Value.ShouldBe(false);
        bookingPolicyService.Received(1).IsBookingAllowed(employeeId, roomType);
    }
}



