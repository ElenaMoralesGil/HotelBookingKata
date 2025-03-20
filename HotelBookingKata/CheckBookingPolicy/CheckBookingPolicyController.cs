
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingKata.CheckBookingPolicy;

[ApiController]
[Route("api/booking-policies")]
public class CheckBookingPolicyController : ControllerBase
{
    private CheckBookingPolicyUseCase useCase;

    public CheckBookingPolicyController(CheckBookingPolicyUseCase useCase)
    {
        this.useCase = useCase;
    }

    [HttpGet("employees/{employeeId}/rooms/{roomType}/allowed")]
    public IActionResult IsBookingAllowed(string employeeId, RoomType roomType)
    {
        try
        {
            bool isAllowed = useCase.Execute(employeeId, roomType);
            return Ok(isAllowed);
        }
        catch (EmployeeNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }
}
