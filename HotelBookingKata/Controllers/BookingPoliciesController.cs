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

    //[HttpPut("companies/{companyId}")]
    //public IActionResult SetCompanyPolicy(string companyId, [FromBody] SetCompanyPolicyRequest request)
    //{
    //    try
    //    {
    //        bookingPolicyService.SetCompanyPolicy(companyId, request.RoomType);
    //        return Ok();
    //    }
    //    catch (CompanyNotFoundException exception)
    //    {
    //        return NotFound(new { message = exception.Message });
    //    }
    //}

    [HttpPut("employees/{employeeId}")]
    public IActionResult SetEmployeePolicy(string employeeId, [FromBody] SetEmployeePolicyRequest request)
    {
        try
        {
            bookingPolicyService.SetEmployeePolicy(employeeId, request.RoomType);
            return Ok();
        }
        catch (EmployeeNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
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
