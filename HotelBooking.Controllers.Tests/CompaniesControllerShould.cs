﻿using HotelBookingKata.Controllers;
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
    public void return_valid_request_when_adding_valid_employee()
    {
        var companyId = "Company1";
        var request = new AddEmployeeRequest("Employee1");


        var result = controller.AddEmployee(companyId, request);

        result.ShouldBeOfType<CreatedResult>();
        var createdResult = (CreatedResult)result;
        createdResult.Location?.ShouldContain(request.EmployeeId);
        companyService.Received(1).AddEmployee(companyId, request.EmployeeId);
    }

    [Test]
    public void return_conflict_when_employee_already_exists()
    {
        var companyId = "Company1";
        var request = new AddEmployeeRequest("Employee1");
        companyService.When(x => x.AddEmployee(companyId, request.EmployeeId)).Throw(new EmployeeAlreadyExistsException("Employee already exists"));
        var result = controller.AddEmployee(companyId, request);
        result.ShouldBeOfType<ConflictObjectResult>();
        companyService.Received(1).AddEmployee(companyId, request.EmployeeId);
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
