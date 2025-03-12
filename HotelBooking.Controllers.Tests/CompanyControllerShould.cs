﻿using HotelBookingKata;
using HotelBookingKata.Services;
using HotelBookingKata.Controllers;
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;
using HotelBookingKata.Exceptions;

namespace HotelBooking.Controllers.Tests
{
    class CompanyControllerShould
    {
        private CompaniesController controller;
        private CompanyService companyService;

        [SetUp]
        public void Setup()
        {
            companyService = Substitute.For<CompanyService>(
                Substitute.For<InMemoryCompanyRepository>(),
                Substitute.For<InMemoryEmployeeRepository>()
                );
            controller = new CompaniesController(companyService);
        }

        [Test]
        public void return_valid_request_when_adding_valid_employee()
        {
            var companyId = "Company1";
            var request =  new AddEmployeeRequest("Employee1");


            var result = controller.AddEmployee(companyId, request);

            result.ShouldBeOfType<CreatedResult>();
            var createdResult = (CreatedResult)result;
            createdResult.Location.ShouldContain(request.EmployeeId);
            companyService.Received(1).AddEmployee(companyId, request.EmployeeId);
        }
    }
}
