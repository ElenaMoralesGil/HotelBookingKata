using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Services;
using HotelBookingKata.SetEmployeePolicy;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests;

class SetEmployeePolicyControllerShould
{

    private SetEmployeePolicyController controller;
    private SetEmployeePolicyUseCase useCase;

    [SetUp]
    public void Setup()
    {
        useCase = Substitute.For<SetEmployeePolicyUseCase>();
        controller = new SetEmployeePolicyController(useCase);
    }
    [Test]
    public void return_ok_when_setting_valid_employee_policy()
    {
        var employeeId = "Employee1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        var request = new SetEmployeePolicyRequest(roomTypes);

        var result = controller.SetEmployeePolicy(employeeId, request);

        result.ShouldBeOfType<OkResult>();
        useCase.Received(1).Execute(employeeId, request);
    }

    [Test]
    public void returns_not_found_when_setting_policy_for_non_existent_employee()
    {
        var employeeId = "Employee1";
        var request = new SetEmployeePolicyRequest(new List<RoomType> { RoomType.Standard });
        useCase.When(x => x.Execute(employeeId, request)).Throw(new EmployeeNotFoundException(employeeId));

        var result = controller.SetEmployeePolicy(employeeId, request);

        result.ShouldBeOfType<NotFoundObjectResult>();
        useCase.Received(1).Execute(employeeId, request);
    }
}
