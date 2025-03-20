using HotelBookingKata.Controllers;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Services;
using Microsoft.AspNetCore.Mvc;

using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests;

class CompaniesControllerShould
{
    private CompaniesController controller;
    private CompanyService companyService;

    [SetUp]
    public void Setup()
    {
        companyService = Substitute.For<CompanyService>();
        controller = new CompaniesController(companyService);
    }

    [Test]
    public void return_valid_request_when_deleting_valid_employee()
    {
        var employeeId = "Employee1";

        var result = controller.DeleteEmployee(employeeId);

        result.ShouldBeOfType<OkResult>();
        companyService.Received(1).DeleteEmployee(employeeId);
    }
}
