using HotelBookingKata;
using HotelBookingKata.Services;
using HotelBookingKata.Controllers;
using HotelBookingKata.Entities;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using HotelBookingKata.Exceptions;

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
    public void return_ok_when_setting_valid_company_policy()
    {
        var companyId = "Company1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        var request = new SetCompanyPolicyRequest(roomTypes);

        var result = controller.SetCompanyPolicy(companyId, request);

        result.ShouldBeOfType<OkResult>();
        bookingPolicyService.Received(1).SetCompanyPolicy(companyId, roomTypes);



    }
}



