using HotelBookingKata.Exceptions;
using HotelBookingKata.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingKata.SetEmployeePolicy;

[ApiController]
[Route("api/booking-policies")]
public class SetEmployeePolicyController : ControllerBase
{
    private SetEmployeePolicyUseCase useCase;

    public SetEmployeePolicyController(SetEmployeePolicyUseCase SetEmployeePolicyUseCase)
    {
        this.useCase = SetEmployeePolicyUseCase;
    }
    [HttpPut("employees/{employeeId}")]
    public IActionResult SetEmployeePolicy(string employeeId, [FromBody] SetEmployeePolicyRequest request)
    {
        try
        {
            useCase.Execute(employeeId, request);
            return Ok();
        }
        catch (EmployeeNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }
}
