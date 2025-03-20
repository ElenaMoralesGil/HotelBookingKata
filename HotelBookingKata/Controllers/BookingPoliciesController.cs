using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingKata.Controllers;

[ApiController]
[Route("api/booking-policies")]
public class BookingPoliciesController : ControllerBase
{
    private BookingPolicyService bookingPolicyService;

    public BookingPoliciesController(BookingPolicyService bookingPolicyService)
    {
        this.bookingPolicyService = bookingPolicyService;
    }

    [HttpGet("employees/{employeeId}/rooms/{roomType}/allowed")]
    public IActionResult IsBookingAllowed(string employeeId, RoomType roomType)
    {
        try
        {
            bool isAllowed = bookingPolicyService.IsBookingAllowed(employeeId, roomType);
            return Ok(isAllowed);
        }
        catch (EmployeeNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }
}
