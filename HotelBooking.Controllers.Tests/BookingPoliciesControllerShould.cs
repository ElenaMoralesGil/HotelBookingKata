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

    [Test]
    public void return_ok_when_setting_valid_employee_policy()
    {
        var employeeId = "Employee1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        var request = new SetEmployeePolicyRequest(roomTypes);
        
        var result = controller.SetEmployeePolicy(employeeId, request);
        
        result.ShouldBeOfType<OkResult>();
        bookingPolicyService.Received(1).SetEmployeePolicy(employeeId, roomTypes);
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


    [Test]
    public void returns_not_found_when_setting_policy_for_non_existent_company()
    {
        var companyId = "Company1";
        var request = new SetCompanyPolicyRequest(new List<RoomType> { RoomType.Standard });
        bookingPolicyService.When(x => x.SetCompanyPolicy(companyId, request.RoomType)).Throw(new CompanyNotFoundException(companyId));

        var result = controller.SetCompanyPolicy(companyId, request);

        result.ShouldBeOfType<NotFoundObjectResult>();
        bookingPolicyService.Received(1).SetCompanyPolicy(companyId, request.RoomType);
    }

    [Test]
    public void returns_not_found_when_setting_policy_for_non_existent_employee()
    {
        var employeeId = "Employee1";
        var request = new SetEmployeePolicyRequest(new List<RoomType> { RoomType.Standard });
        bookingPolicyService.When(x => x.SetEmployeePolicy(employeeId, request.RoomType)).Throw(new EmployeeNotFoundException(employeeId));
        
        var result = controller.SetEmployeePolicy(employeeId, request);
        
        result.ShouldBeOfType<NotFoundObjectResult>();
        bookingPolicyService.Received(1).SetEmployeePolicy(employeeId, request.RoomType);
    }



}



