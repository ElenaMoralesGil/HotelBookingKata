using HotelBookingKata.AddEmployee;
using HotelBookingKata.Exceptions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests
{
    class AddEmployeeControllerShould
    {
        private AddEmployeeController controller;
        private AddEmployeeUseCase useCase;

        [SetUp]
        public void Setup()
        {
            useCase = Substitute.For<AddEmployeeUseCase>();
            controller = new AddEmployeeController(useCase);
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
            useCase.Received(1).Execute(companyId, request);
        }

        [Test]
        public void return_conflict_when_employee_already_exists()
        {
            var companyId = "Company1";
            var request = new AddEmployeeRequest("Employee1");
            useCase.When(x => x.Execute(companyId, request)).Throw(new EmployeeAlreadyExistsException("Employee already exists"));
            var result = controller.AddEmployee(companyId, request);
            result.ShouldBeOfType<ConflictObjectResult>();
            useCase.Received(1).Execute(companyId, request);
        }
    }
}
