using Microsoft.AspNetCore.Mvc;
using HotelBookingKata.DeleteEmployee;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Controllers.Tests;

class DeleteEmployeeControllerShould
{
    private DeleteEmployeeController controller;
    private DeleteEmployeeUseCase useCase;

    [SetUp]
    public void Setup()
    {
        useCase = Substitute.For<DeleteEmployeeUseCase>();
        controller = new DeleteEmployeeController(useCase);
    }

    [Test]
    public void return_valid_request_when_deleting_valid_employee()
    {
        var employeeId = "Employee1";

        var result = controller.DeleteEmployee(employeeId);

        result.ShouldBeOfType<OkResult>();
        useCase.Received(1).Execute(employeeId);
    }
}
